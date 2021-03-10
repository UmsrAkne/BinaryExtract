using BinaryExtract.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;

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
                    SystemMessage = "数値の変換に失敗しました";
                }

            }));
        }
        private DelegateCommand searchCommand;
        #endregion

    }
}
