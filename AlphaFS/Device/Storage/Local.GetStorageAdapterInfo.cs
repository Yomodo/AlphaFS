/*  Copyright (C) 2008-2018 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy 
 *  of this software and associated documentation files (the "Software"), to deal 
 *  in the Software without restriction, including without limitation the rights 
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 *  copies of the Software, and to permit persons to whom the Software is 
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 *  THE SOFTWARE. 
 */

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.AccessControl;
using Alphaleonis.Win32.Filesystem;
using Microsoft.Win32.SafeHandles;

namespace Alphaleonis.Win32.Device
{
   public static partial class Local
   {
      /// <summary>[AlphaFS] Retrieves the type, device- and partition number for the storage device on the Computer that is related to the logical drive name, volume <see cref="Guid"/> or <see cref="DeviceInfo.DevicePath"/>.
      /// <para>Calling this method requires an elevated state.</para>
      /// </summary>
      /// <returns>Returns a <see cref="StorageDeviceInfo"/> instance that represent the storage device on the Computer that is related to <paramref name="devicePath"/>.</returns>
      /// <exception cref="ArgumentException"/>
      /// <exception cref="ArgumentNullException"/>
      /// <exception cref="NotSupportedException"/>
      /// <exception cref="Exception"/>
      /// <param name="devicePath">
      /// <para>A disk path such as: <c>\\.\PhysicalDrive0</c></para>
      /// <para>A drive path such as: <c>C</c>, <c>C:</c> or <c>C:\</c></para>
      /// <para>A volume <see cref="Guid"/> such as: <c>\\?\Volume{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}\</c></para>
      /// <para>A <see cref="DeviceInfo.DevicePath"/> string such as: <c>\\?\pcistor#disk...{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}</c></para>
      /// </param>
      public static StorageAdapterInfo GetStorageAdapterInfo(string devicePath)
      {
         var pathToDevice = FileSystemHelper.GetDevicePath(devicePath);

         if (Utils.IsNullOrWhiteSpace(pathToDevice))
            return null;


         using (var safeHandle = FileSystemHelper.OpenPhysicalDisk(pathToDevice, FileSystemRights.Read))

            return GetStorageAdapterInfoNative(safeHandle, devicePath);
      }


      [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
      private static StorageAdapterInfo GetStorageAdapterInfoNative(SafeFileHandle safeHandle, string pathToDevice)
      {
         var storagePropertyQuery = new NativeMethods.STORAGE_PROPERTY_QUERY
         {
            PropertyId = NativeMethods.STORAGE_PROPERTY_ID.StorageAdapterProperty,
            QueryType = NativeMethods.STORAGE_QUERY_TYPE.PropertyStandardQuery
         };

         SafeFileHandle safeHandleRetry = null;
         var isRetry = false;


      StartGetData:
         
         // Get storage adapter info.

         using (var safeBuffer = InvokeDeviceIoData(isRetry ? safeHandleRetry : safeHandle, NativeMethods.IoControlCode.IOCTL_STORAGE_QUERY_PROPERTY, storagePropertyQuery, pathToDevice, Filesystem.NativeMethods.DefaultFileBufferSize / 8))
         {
            //if (null == safeBuffer)
            //{
            //   // Assumption through observation: devicePath is a logical drive that points to a Dynamic disk.


            //   var volDiskExtents = GetVolumeDiskExtents(isRetry ? safeHandleRetry : safeHandle, pathToDevice);

            //   if (volDiskExtents.HasValue)
            //   {
            //      // Use the first disk extent.

            //      pathToDevice = string.Format(CultureInfo.InvariantCulture, "{0}{1}", Path.PhysicalDrivePrefix, volDiskExtents.Value.Extents[0].DiskNumber.ToString(CultureInfo.InvariantCulture));


            //      safeHandleRetry = FileSystemHelper.OpenPhysicalDisk(pathToDevice, FileSystemRights.Read);

            //      isRetry = Utils.IsValidHandle(safeHandleRetry, false);
            //   }


            //   if (isRetry)
            //      goto StartGetData;

            //   return null;
            //}


            if (isRetry && !safeHandleRetry.IsClosed)
               safeHandleRetry.Close();

            return new StorageAdapterInfo(safeBuffer.PtrToStructure<NativeMethods.STORAGE_ADAPTER_DESCRIPTOR>());
         }
      }
   }
}
