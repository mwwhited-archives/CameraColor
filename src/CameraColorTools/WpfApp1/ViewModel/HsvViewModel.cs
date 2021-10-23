using System;
using WpfApp1.Drawings;

namespace WpfApp1.ViewModel
{
    public class HsvViewModel : ViewModelBase
    {
        private ColorViewModel _color;
        private bool _skip;

        public HsvViewModel(ColorViewModel color)
        {
            this._color = color;
            this.PropertyChanged += _PropertyChanged;
        }

        private void _PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!_skip)
            {
                _skip = true;
                var current = (Hue, Saturation / 100.0, Value / 100.0);

                var rgb = ColorConversion.Hsv2Rgb(current, 255.0);
                _color.RGB.Set(rgb);

                _skip = false;
            }
        }

        internal void Set((double hue, double saturation, double value) hsv)
        {
            if (!_skip)
            {
                _skip = true;

                this.Hue = hsv.hue;
                this.Saturation = hsv.saturation * 100.0;
                this.Value = hsv.value * 100.0;

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
        public double Value
        {
            get => this.Property.Get(() => 0.0);
            set => this.Property.Set(value);
        }
    }
}
