using System.ComponentModel;
using WpfApp1.Drawings;

namespace WpfApp1.ViewModel
{
    public class RybViewModel : ViewModelBase
    {
        private ColorViewModel _color;
        private bool _skip;

        public RybViewModel(ColorViewModel color)
        {
            this._color = color;

            this.PropertyChanged += _PropertyChanged;
        }

        private void _PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_skip)
            {
                _skip = true;
                var current = (Red / 100.0, Yellow / 100.0, Blue / 100.0);

                var rgb = ColorConversion.Ryb2Rgb(current, 1.0);
                _color.RGB.Set(rgb);

                _skip = false;
            }
        }

        internal void Set((double red, double yellow, double blue) rgb)
        {
            if (!_skip)
            {
                _skip = true;

                this.Red = rgb.red * 100.0;
                this.Yellow = rgb.yellow * 100.0;
                this.Blue = rgb.blue * 100.0;

                _skip = false;
            }
        }

        public double Red
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
        public double Yellow
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
        public double Blue
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
    }
}
