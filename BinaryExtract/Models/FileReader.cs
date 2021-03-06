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

        public DirectoryInfo OutputDirectoryInfo { get; set; } = new DirectoryInfo("output");
        public string OutputFileExtension { get; set; } = "";

        public List<String> Message { private set; get; } = new List<String>();

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

            if(positions.Count == 0) {
                Message.Add("一致するパターンはありません");
            }

            return positions;
        }

        public void split(List<Byte> searchBytes) {
            var spPositions = search(searchBytes);

            if (!OutputDirectoryInfo.Exists) {
                OutputDirectoryInfo.Create();
            }

            if(spPositions.Count == 0) {
                return;
            }

            fileStream.Position = spPositions[0];

            // ファイル終端のアドレスを加える。
            // ループを回す際、最後の分割位置からファイル終端までの区間もファイル化するのに必要。
            spPositions.Add(TargetFileInfo.Length);

            for(int i = 0; i < spPositions.Count -1; i++) {

                // 分割位置 (spPositions) の値の下限(i)から上限(i+1)までを読み込み。配列に詰める。
                byte[] arr = new byte[spPositions[i + 1] - spPositions[i]];
                fileStream.Read(arr, 0, Convert.ToInt32(spPositions[i + 1] - spPositions[i]));

                string fileFullName = $"{OutputDirectoryInfo.FullName}\\{String.Format("{0:00000}", i)}{OutputFileExtension}";

                using (FileStream fs = File.Create(fileFullName)) {
                    Message.Add($"{String.Format("{0:00000}", i)} を生成したました。");
                    fs.Write(arr, 0, arr.Length);
                }
            }
        }
    }
}
