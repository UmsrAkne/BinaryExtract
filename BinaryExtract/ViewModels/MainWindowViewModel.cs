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
        private string _title = "Prism Application";
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
                SetProperty(ref currentFileInfo, value);
            }
        }

        private string searchPattern = "";
        public string SearchPattern {
            get => searchPattern;
            set => SetProperty(ref searchPattern, value);
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
                List<Byte> list;
                
                try {
                    list = HexConverter.convertHexToDecimals(SearchPattern);
                }
                catch (ArgumentException) {
                    SystemMessage += "\r数値の変換に失敗しました";
                }

            }));
        }
        private DelegateCommand searchCommand;
        #endregion


        public DelegateCommand SplitCommand {
            #region
            get => splitCommand ?? (splitCommand = new DelegateCommand(() => {
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
