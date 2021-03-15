using BinaryExtract.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BinaryExtract.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "BinaryExtract";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private FileInfo currentFileInfo;
        public FileInfo CurrentFileInfo {
            get => currentFileInfo; 
            set {
                SystemMessage = $"{value.FullName} を読み込みました";
                FileReader = new FileReader(value);
                OutputDirectoryPath = FileReader.OutputDirectoryInfo.FullName;
                SetProperty(ref currentFileInfo, value);
            }
        }

        private string searchPattern = "";
        public string SearchPattern {
            get => searchPattern;
            set => SetProperty(ref searchPattern, value);
        }

        private string outputDirectoryPath = "";
        public string OutputDirectoryPath {
            get => outputDirectoryPath;
            set => SetProperty(ref outputDirectoryPath, value);
        }

        private string outputFileExtension = "";
        public string OutputFileExtension {
            get => outputFileExtension;
            set => SetProperty(ref outputFileExtension, value);
        }

        private string systemMessage;
        public string SystemMessage {
            get => systemMessage;
            set => SetProperty(ref systemMessage, value);
        }

        private HexConverter HexConverter { get; } = new HexConverter();

        private FileReader FileReader { set; get; } 

        private List<Byte> HexSearchPattern {
            get => HexConverter.convertHexToDecimals(SearchPattern);
        }

        public MainWindowViewModel() {
        }


        public DelegateCommand SearchCommand {
            #region
            get => searchCommand ?? (searchCommand = new DelegateCommand(() => {
                List<Byte> list = new List<Byte>();
                
                try {
                    list = HexConverter.convertHexToDecimals(SearchPattern);
                }
                catch (ArgumentException) {
                    SystemMessage += "\r数値の変換に失敗しました";
                }

                if(list.Count > 0) {
                    var positions = FileReader.search(list);
                    var sb = new StringBuilder();
                    positions.ForEach(p => sb.AppendLine(String.Format("{0:00000000}",p)));
                    SystemMessage += $"\rマッチ {positions.Count} 件";
                    SystemMessage += $"\r{sb}";
                }
            }));
        }
        private DelegateCommand searchCommand;
        #endregion


        public DelegateCommand SplitCommand {
            #region
            get => splitCommand ?? (splitCommand = new DelegateCommand(() => {
                FileReader.OutputDirectoryInfo = new DirectoryInfo(OutputDirectoryPath);
                FileReader.OutputFileExtension = OutputFileExtension;
                FileReader.split(HexSearchPattern);

                var strBuilder = new StringBuilder();
                FileReader.Message.ForEach(str => strBuilder.AppendLine(str));
                SystemMessage += $"\r{strBuilder}";

            }));
        }
        private DelegateCommand splitCommand;
        #endregion

    }
}
