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
            set => SetProperty(ref currentFileInfo, value);
        }

        public MainWindowViewModel() {

        }
    }
}
