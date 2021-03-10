using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryExtract.Models {
    public class HexConverter {

        /// <summary>
        /// スペースで区切られた １６進数の文字列を１０進数の数値のリストに変換します。
        /// </summary>
        /// <param name="hex"> Byte 型の範囲の 16進数の文字列　例:"00 ff 01 aa" </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">引数内に、Byteの範囲の数値に変換できない文字を含む場合にスローされます。</exception>
        public List<Byte> convertHexToDecimals(string hex) {
            var hexStrings = hex.Split(' ');
            var decimals = new List<Byte>();

            for(int i = 0; i < hexStrings.Length; i++) {
                
                if(!Byte.TryParse(hexStrings[i],System.Globalization.NumberStyles.HexNumber, null, out Byte result)) { 
                    throw new ArgumentException("指定された文字は、 Byte に変換できません");
                }
                else {
                    decimals.Add(result);
                }
            }

            return decimals;
        }

        /// <summary>
        /// スペースで区切られた１０進数の文字列を、１０進数の数値のリストに変換します。
        /// </summary>
        /// <param name="decimals">Byte型の範囲の１０進数の文字列 例:"00 111 255"</param>
        /// <returns></returns>
        public List<Byte> toDecimals(string decimals) {
            var decimalStrings = decimals.Split(' ');
            var list = new List<Byte>();

            for(int i = 0; i < decimalStrings.Length; i++) {
                if (!Byte.TryParse(decimalStrings[i],out Byte result)) {
                    throw new ArgumentException("指定された文字は、 Byte に変換できません");
                }
                else {
                    list.Add(result);
                }
            }

            return list;
        }
    }
}
