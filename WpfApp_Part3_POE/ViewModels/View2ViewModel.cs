using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp_Part3_POE.ViewModels
{
    public class View2ViewModel : INotifyPropertyChanged
    {
        private string userInput;

        public string UserInput
        {
            get => userInput;
            set
            {
                userInput = value;
                OnPropertyChanged(nameof(UserInput));
            }
        }

        public ICommand SubmitCommand { get; }

        public View2ViewModel()
        {
            SubmitCommand = new RelayCommand(Submit);
        }

        private void Submit(object parameter)
        {
            // Handle submit logic here
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

