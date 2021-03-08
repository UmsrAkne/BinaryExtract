using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryExtract.Models {
    class FileReader {

        public FileInfo TargetFileInfo{ get; private set; }
        private FileStream fileStream;

        public FileReader(FileInfo targetFileInfo) {
            this.TargetFileInfo = targetFileInfo;
            fileStream = new FileStream(TargetFileInfo.FullName, FileMode.Open, FileAccess.Read);
        }

    }
}
