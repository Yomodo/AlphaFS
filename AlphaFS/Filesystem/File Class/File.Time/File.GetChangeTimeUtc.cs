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

using Microsoft.Win32.SafeHandles;
using System;
using System.Security;

namespace Alphaleonis.Win32.Filesystem
{
   public static partial class File
   {
      /// <summary>[AlphaFS] Gets the change date and time, in Coordinated Universal Time (UTC) format, of the specified file.</summary>
      /// <returns>A <see cref="DateTime"/> structure set to the change date and time for the specified file. This value is expressed in UTC time.</returns>
      /// <exception cref="PlatformNotSupportedException">The operating system is older than Windows Vista.</exception>
      /// <param name="path">The file for which to obtain change date and time information, in Coordinated Universal Time (UTC) format.</param>
      [SecurityCritical]
      public static DateTime GetChangeTimeUtc(string path)
      {
         return GetChangeTimeCore(null, null, path, true, PathFormat.RelativePath);
      }


      /// <summary>[AlphaFS] Gets the change date and time, in Coordinated Universal Time (UTC) format, of the specified file.</summary>
      /// <returns>A <see cref="DateTime"/> structure set to the change date and time for the specified file. This value is expressed in UTC time.</returns>
      /// <exception cref="PlatformNotSupportedException">The operating system is older than Windows Vista.</exception>
      /// <param name="path">The file for which to obtain change date and time information, in Coordinated Universal Time (UTC) format.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>
      [SecurityCritical]
      public static DateTime GetChangeTimeUtc(string path, PathFormat pathFormat)
      {
         return GetChangeTimeCore(null, null, path, true, pathFormat);
      }


      /// <summary>[AlphaFS] Gets the change date and time, in Coordinated Universal Time (UTC) format, of the specified file.</summary>
      /// <returns>A <see cref="DateTime"/> structure set to the change date and time for the specified file. This value is expressed in UTC time.</returns>
      /// <exception cref="PlatformNotSupportedException">The operating system is older than Windows Vista.</exception>
      /// <param name="safeFileHandle">An open handle to the file or directory from which to retrieve information.</param>
      [SecurityCritical]
      public static DateTime GetChangeTimeUtc(SafeFileHandle safeFileHandle)
      {
         return GetChangeTimeCore(null, safeFileHandle, null, true, PathFormat.LongFullPath);
      }
   }
}
