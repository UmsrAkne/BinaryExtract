using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryExtract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BinaryExtract.Models.Tests {
    [TestClass()]
    public class FileReaderTests {
        [TestMethod()]
        public void searchTest() {
            var dummyFile = new FileInfo("dummyText.txt");
            if (!dummyFile.Exists) {
                using (StreamWriter sw = dummyFile.CreateText()) {
                    sw.WriteLine("This is dummy text");
                }
            }

            var fileReader = new FileReader(new FileInfo("dummyText.txt"));
            fileReader.search(null);
            fileReader.search(new List<Byte>(new Byte[] { 115, 32, 105 }));
        }
    }
}