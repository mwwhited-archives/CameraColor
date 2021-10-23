using System.ComponentModel;
using System.Windows.Media;
using WpfApp1.Drawings;

namespace WpfApp1.ViewModel
{
    public class RgbViewModel : ViewModelBase
    {
        private ColorViewModel _color;
        private bool _skip;

        public RgbViewModel(ColorViewModel color)
        {
            this._color = color;
            this.PropertyChanged += _PropertyChanged;
        }

        private void _PropertyChanged(object sender, PropertyChangedEventArgs e) => UpdateOthers();

        internal void Set((double red, double green, double blue) rgb)
        {
            if (!_skip)
            {
                _skip = true;

                this.Red = rgb.red;
                this.Green = rgb.green;
                this.Blue = rgb.blue;
                _skip = false;

                UpdateOthers();
            }
        }

        internal void UpdateOthers()
        {
            if (!_skip)
            {
                _skip = true;
                var current = (Red, Green, Blue);

                var hsl = ColorConversion.Rgb2Hsl(current, 255.0);
                var hsv = ColorConversion.Rgb2Hsv(current, 255.0);
                var cmyk = ColorConversion.Rgb2Cmyk(current, 255.0);
                var ryb = ColorConversion.Rgb2Ryb(current, 255.0);
                var cmy = ColorConversion.Rgb2Cmy(current, 255.0);
                var xyz = ColorConversion.Rgb2Xyz(current, 255.0);

                _color.HSL.Set(hsl);
                _color.HSV.Set(hsv);
                _color.CMYK.Set(cmyk);
                _color.RYB.Set(ryb);
                _color.CMY.Set(cmy);
                _color.XYZ.Set(xyz);

                _skip = false;
            }
        }

        public double Red
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
        public double Green
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
        public double Blue
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }

        public Color Color => this.Property.Calculated(() => Color.FromRgb((byte)Red, (byte)Green, (byte)Blue));
    }
}
