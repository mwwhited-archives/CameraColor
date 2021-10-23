using System.ComponentModel;
using WpfApp1.Drawings;

namespace WpfApp1.ViewModel
{
    public class XyzViewModel : ViewModelBase
    {
        private ColorViewModel _color;
        private bool _skip;

        public XyzViewModel(ColorViewModel color)
        {
            this._color = color;

            this.PropertyChanged += _PropertyChanged;
        }

        private void _PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_skip)
            {
                _skip = true;
                var current = (X / 100.0, Y / 100.0, Z / 100.0);

                var rgb = ColorConversion.Xyz2Rgb(current, 255.0);
                _color.RGB.Set(rgb);

                _skip = false;
            }
        }

        internal void Set((double x, double y, double z) xyz)
        {
            if (!_skip)
            {
                _skip = true;

                this.X = xyz.x * 100.0;
                this.Y = xyz.y * 100.0;
                this.Z = xyz.z * 100.0;

                _skip = false;
            }
        }

        public double X
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
        public double Y
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
        public double Z
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }

    }
}
