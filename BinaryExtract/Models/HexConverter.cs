using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryExtract.Models {
    public class HexConverter {
        public List<Byte> convertHexToDecimals(string hex) {
            var hexStrings = hex.Split(' ');
            var decimals = new List<Byte>();

            for(int i = 0; i < hexStrings.Length; i++) {
                decimals.Add(Convert.ToByte(hexStrings[i], 16));
            }

            return decimals;
        }
    }
}
