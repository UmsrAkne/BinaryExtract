using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryExtract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryExtract.Models.Tests {
    [TestClass()]
    public class HexConverterTests {
        [TestMethod()]
        public void convertHexToDecimalsTest() {
            HexConverter hexConverter = new HexConverter();
            var list = hexConverter.convertHexToDecimals("00 0a ff");

            Assert.AreEqual(list[0], 0);
            Assert.AreEqual(list[1], 10);
            Assert.AreEqual(list[2], 255);

            var argumentError = false;
            try {
                hexConverter.convertHexToDecimals("iii");
            }
            catch (ArgumentException) {
                argumentError = true;
            }

            Assert.IsTrue(argumentError);
        }

        [TestMethod()]
        public void toDecimalsTest() {
            HexConverter hexConverter = new HexConverter();
            var list = hexConverter.toDecimals("00 10 255");

            Assert.AreEqual(list[0], 0);
            Assert.AreEqual(list[1], 10);
            Assert.AreEqual(list[2], 255);

            var argumentError = false;
            try {
                hexConverter.toDecimals("aa");
            }
            catch (ArgumentException) {
                argumentError = true;
            }

            Assert.IsTrue(argumentError);
        }
    }
}