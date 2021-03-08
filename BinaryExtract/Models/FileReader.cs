using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryExtract.Models {
    public class FileReader {

        public FileInfo TargetFileInfo{ get; private set; }
        private FileStream fileStream;

        public FileReader(FileInfo targetFileInfo) {
            this.TargetFileInfo = targetFileInfo;
            fileStream = new FileStream(TargetFileInfo.FullName, FileMode.Open, FileAccess.Read);
        }

        /// <summary>
        /// TargetFileInfo のバイト配列を読み込み、引数に指定したバイト配列に一致する部分を検索します。
        /// </summary>
        /// <param name="searchBytes">検索対象とするバイト配列を入力します。</param>
        /// <returns>検索して見つかったバイト配列の先頭アドレスを詰めたリストを返します。</returns>
        public List<long> search(List<Byte> searchBytes) {
            if(searchBytes == null || searchBytes.Count == 0) {
                throw new ArgumentException("要素０のリスト、nullは引数にはできません");
            }

            int readByte = 0;
            int matchedCount = 0;
            var positions = new List<long>();

            while (readByte >= 0) {
                readByte = fileStream.ReadByte();

                if(readByte == searchBytes[matchedCount]) {
                    matchedCount++;
                    if(matchedCount == searchBytes.Count) {
                        positions.Add(fileStream.Position - matchedCount);
                        matchedCount = 0;
                    }
                }
                else {
                    matchedCount = 0;
                }

            }

            return positions;
        }

    }
}
