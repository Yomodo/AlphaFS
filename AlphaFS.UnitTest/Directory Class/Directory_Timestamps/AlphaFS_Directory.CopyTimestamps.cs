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
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlphaFS.UnitTest
{
   public partial class TimestampsTest
   {
      // Pattern: <class>_<function>_<scenario>_<expected result>


      [TestMethod]
      public void AlphaFS_Directory_CopyTimestamps_LocalAndNetwork_Success()
      {
         AlphaFS_Directory_CopyTimestamps(false);
         AlphaFS_Directory_CopyTimestamps(true);
      }

      
      private void AlphaFS_Directory_CopyTimestamps(bool isNetwork)
      {
         using (var tempRoot = new TemporaryDirectory(isNetwork))
         {
            var folder1 = tempRoot.RandomDirectoryFullPath;
            var folder2 = tempRoot.RandomDirectoryFullPath;

            System.IO.Directory.CreateDirectory(folder1);
            Thread.Sleep(1500);
            System.IO.Directory.CreateDirectory(folder2);


            Console.WriteLine("Input Directory1 Path: [{0}]", folder1);
            Console.WriteLine("Input Directory2 Path: [{0}]", folder2);


            Assert.AreNotEqual(System.IO.Directory.GetCreationTime(folder1), System.IO.Directory.GetCreationTime(folder2));
            Assert.AreNotEqual(System.IO.Directory.GetLastAccessTime(folder1), System.IO.Directory.GetLastAccessTime(folder2));
            Assert.AreNotEqual(System.IO.Directory.GetLastWriteTime(folder1), System.IO.Directory.GetLastWriteTime(folder2));


            Alphaleonis.Win32.Filesystem.Directory.CopyTimestamps(folder1, folder2);


            Assert.AreEqual(System.IO.Directory.GetCreationTime(folder1), System.IO.Directory.GetCreationTime(folder2));
            Assert.AreEqual(System.IO.Directory.GetLastAccessTime(folder1), System.IO.Directory.GetLastAccessTime(folder2));
            Assert.AreEqual(System.IO.Directory.GetLastWriteTime(folder1), System.IO.Directory.GetLastWriteTime(folder2));
         }

         Console.WriteLine();
      }
   }
}
