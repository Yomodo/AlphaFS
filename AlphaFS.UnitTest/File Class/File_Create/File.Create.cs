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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlphaFS.UnitTest
{
   public partial class File_CreateTest
   {
      // Pattern: <class>_<function>_<scenario>_<expected result>


      [TestMethod]
      public void File_Create_LocalAndNetwork_Success()
      {
         File_Create(false);
         File_Create(true);
      }


      private void File_Create(bool isNetwork)
      {
         using (var tempRoot = new TemporaryDirectory(isNetwork))
         {
            var file = tempRoot.RandomFileFullPath;

#if NET35
            // MSDN: .NET 4+ Trailing spaces are removed from the end of the path parameter before deleting the directory.
            file += UnitTestConstants.EMspace;
#endif

            Console.WriteLine("Input File Path: [{0}]\n", file);


            long fileLength;
            var ten = UnitTestConstants.TenNumbers.Length;

            using (var fs = Alphaleonis.Win32.Filesystem.File.Create(file))
            {
               // According to NotePad++, creates a file type: "ANSI", which is reported as: "Unicode (UTF-8)".
               fs.Write(UnitTestConstants.StringToByteArray(UnitTestConstants.TenNumbers), 0, ten);

               fileLength = fs.Length;
            }

            Assert.IsTrue(System.IO.File.Exists(file), "File should exist.");
            Assert.IsTrue(fileLength == ten, "The file is: {0} bytes, but is expected to be: {1} bytes.", fileLength, ten);
         }

         Console.WriteLine();
      }
   }
}
