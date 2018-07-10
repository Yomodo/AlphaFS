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

using System.Collections.Generic;
using Alphaleonis.Win32.Filesystem;

namespace Alphaleonis.Win32.Device
{
   public static partial class Local
   {
      /// <summary>[AlphaFS] Returns an enumerable collection of directory names in a specified path.</summary>
      /// <param name="portableDeviceInfo">The <see cref="T:PortableDeviceInfo"/> instance of the Portable Device.</param>
      public static IEnumerable<WpdFileSystemInfo> EnumerateDirectories(PortableDeviceInfo portableDeviceInfo)
      {
         return PortableDeviceInfo.EnumerateFileSystemEntryInfoCore(portableDeviceInfo, null, null, false, true);
      }


      /// <summary>[AlphaFS] Returns an enumerable collection of directory names in a specified path.</summary>
      /// <param name="portableDeviceInfo">The <see cref="T:PortableDeviceInfo"/> instance of the Portable Device.</param>
      /// <param name="recurse"></param>
      public static IEnumerable<WpdFileSystemInfo> EnumerateDirectories(PortableDeviceInfo portableDeviceInfo, bool recurse)
      {
         return PortableDeviceInfo.EnumerateFileSystemEntryInfoCore(portableDeviceInfo, null, null, recurse, true);
      }


      /// <summary>[AlphaFS] Returns an enumerable collection of directory names in a specified path.</summary>
      /// <param name="portableDeviceInfo">The <see cref="T:PortableDeviceInfo"/> instance of the Portable Device.</param>
      /// <param name="objectId">The ID of the directory to search.</param>
      /// <param name="recurse"></param>
      public static IEnumerable<WpdFileSystemInfo> EnumerateDirectories(PortableDeviceInfo portableDeviceInfo, string objectId, bool recurse)
      {
         return PortableDeviceInfo.EnumerateFileSystemEntryInfoCore(portableDeviceInfo, null, objectId, recurse, true);
      }
   }
}