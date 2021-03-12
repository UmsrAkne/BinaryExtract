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

        [TestMethod()]
        public void splitTest() {
            ///　下記ファイルのバイナリ値は
            /// 84 104 105 115 32 105 115 32 100 117 109 109 121 32 116 101 120 116 13 10

            var dummyFile = new FileInfo("dummyText.txt");
            if (!dummyFile.Exists) {
                using (StreamWriter sw = dummyFile.CreateText()) {
                    sw.WriteLine("This is dummy text");
                }
            }

            var fileReader = new FileReader(new FileInfo("dummyText.txt"));

            fileReader.split(new List<Byte>(new Byte[] { 32 }));

            using (FileStream fr = new FileStream("00000",FileMode.Open)) {
                Assert.AreEqual(fr.ReadByte(), 32);
                Assert.AreEqual(fr.ReadByte(), 105);
                Assert.AreEqual(fr.ReadByte(), 115);
                Assert.AreEqual(fr.Length,3,"最初に出現する 32 から、次の 32 までの 3バイト");
            }

            using (FileStream fr = new FileStream("00001",FileMode.Open)) {
                Assert.AreEqual(fr.ReadByte(), 32);
                Assert.AreEqual(fr.ReadByte(), 100);
                Assert.AreEqual(fr.ReadByte(), 117);
                Assert.AreEqual(fr.Length,6);
            }
        }
    }
}