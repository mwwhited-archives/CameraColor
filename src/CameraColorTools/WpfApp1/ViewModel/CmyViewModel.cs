using System.ComponentModel;
using WpfApp1.Drawings;

namespace WpfApp1.ViewModel
{
    public class CmyViewModel : ViewModelBase
    {
        private ColorViewModel _color;
        private bool _skip;

        public CmyViewModel(ColorViewModel color)
        {
            this._color = color;
            this.PropertyChanged += _PropertyChanged;
        }

        private void _PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_skip)
            {
                _skip = true;
                var current = (Cyan / 100.0, Magenta / 100.0, Yellow / 100.0);

                var rgb = ColorConversion.Cmy2Rgb(current, 255.0);
                _color.RGB.Set(rgb);

                _skip = false;
            }
        }

        internal void Set((double red, double magenta, double yellow) color)
        {
            if (!_skip)
            {
                _skip = true;

                this.Cyan = color.red * 100.0;
                this.Yellow = color.yellow * 100.0;
                this.Magenta = color.magenta * 100.0;

                _skip = false;
            }
        }

        public double Cyan
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
        public double Magenta
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
        public double Yellow
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
    }
}
