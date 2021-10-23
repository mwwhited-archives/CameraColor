using Nito.Mvvm.CalculatedProperties;
using System.ComponentModel;

namespace WpfApp1.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected PropertyHelper Property { get; }

        protected ViewModelBase()
        {
            Property = new PropertyHelper(RaisePropertyChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
    }
}
