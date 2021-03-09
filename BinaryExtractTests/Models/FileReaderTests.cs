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

            var nullError = false;
            try {
                fileReader.search(null);
            }
            catch (ArgumentException) {
                nullError = true;
            }

            var zeroLengthError = false;
            try {
                fileReader.search(new List<Byte>());
            }
            catch (ArgumentException) {
                zeroLengthError = true;
            }

            Assert.IsTrue(nullError);
            Assert.IsTrue(zeroLengthError);

            List<long> result = fileReader.search(new List<Byte>(new Byte[] { 115, 32, 105 }));
            Assert.AreEqual(result.First(), 3);
        }
    }
}