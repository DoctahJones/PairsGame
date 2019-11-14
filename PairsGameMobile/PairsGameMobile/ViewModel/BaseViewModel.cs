using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PairsGameMobile.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public BaseViewModel()
        {
        }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                if (title == value)
                {
                    return;
                }
                title = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy == value)
                {
                    return;
                }
                isBusy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public bool IsNotBusy => !IsBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
