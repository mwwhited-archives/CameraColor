namespace WpfApp1.ViewModel
{
    public class ColorViewModel : ViewModelBase
    {
        public ColorViewModel()
        {
            RGB = new RgbViewModel(this);
            HSV = new HsvViewModel(this);
            HSL = new HslViewModel(this);
            RYB = new RybViewModel(this);
            CMYK = new CmykViewModel(this);
            CMY = new CmyViewModel(this);

            this.RGB.UpdateOthers();
        }

        public RgbViewModel RGB { get; }
        public HsvViewModel HSV { get; }
        public HslViewModel HSL { get; }
        public RybViewModel RYB { get; }
        public CmykViewModel CMYK { get; }
        public CmyViewModel CMY { get; }
    }
}
