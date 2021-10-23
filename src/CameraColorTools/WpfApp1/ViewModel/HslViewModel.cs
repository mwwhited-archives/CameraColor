using WpfApp1.Drawings;

namespace WpfApp1.ViewModel
{
    public class HslViewModel : ViewModelBase
    {
        private ColorViewModel _color;
        private bool _skip;

        public HslViewModel(ColorViewModel color)
        {
            this._color = color;
            this.PropertyChanged += _PropertyChanged;
        }

        private void _PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!_skip)
            {
                _skip = true;
                var current = (Hue, Saturation / 100.0, Lightness / 100.0);

                var rgb = ColorConversion.Hsl2Rgb(current, 255.0);
                _color.RGB.Set(rgb);

                _skip = false;
            }
        }

        internal void Set((double hue, double saturation, double lightness) hsl)
        {
            if (!_skip)
            {
                _skip = true;

                this.Hue = hsl.hue;
                this.Saturation = hsl.saturation * 100.0;
                this.Lightness = hsl.lightness * 100.0;

                _skip = false;
            }
        }

        public double Hue
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
        public double Saturation
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
        public double Lightness
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
    }
}
