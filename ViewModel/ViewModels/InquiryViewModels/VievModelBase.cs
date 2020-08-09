using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ViewModel.ViewModels
{
        public class VievModelBase : INotifyPropertyChanged {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
}