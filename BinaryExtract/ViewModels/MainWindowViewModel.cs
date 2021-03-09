using Prism.Mvvm;
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

        private string systemMessage;
        public string SystemMessage {
            get => systemMessage;
            set => SetProperty(ref systemMessage, value);
        }

        public MainWindowViewModel() {

        }
    }
}
