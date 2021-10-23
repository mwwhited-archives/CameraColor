using System.IO;
using System.Runtime.InteropServices;

namespace NikonFormats
{
    class Program
    {
        static void Main(string[] args)
        {
            var nikon = new NikonFormats();
        }

        public static void PictureControl()
        {
            var nikon = new NikonFormats();
            var data = nikon.PictureControl(
                description: "Matt Whited",
                sharpening: 3,
                customCurve: true,
                contrast: null,
                brightness: null,
                saturation: 0,
                hue: 0,
                points: new[]
                {
                    (0x00, 0x00),
                    (0x1D, 0x06),
                    (0x3D, 0x1A),
                    (0x56, 0x3A),
                    (0x7F, 0x7F),
                    (0xC7, 0xE5),
                    (0xDF, 0xF7),
                    (0xFF, 0xFF),
                }
                );
            File.WriteAllBytes(@"C:\Users\MattWhited\OneDrive - Personal\OneDrive\Nikon\Mine.ncp", data);
        }
    }

    public class NikonSettings
    {
        [Offset(0x00)]
        public string Manufacture = "Nikon";
        [Offset(0x005)]
        public byte[] Reserved0 = new byte[] { 0, 0, 1 };
        [Offset(0x008)]
        public string Model = "D800";

        [Offset(0x123),Length(36)]
        public string Comment = "";
        [Offset(0x147), Length(36)]
        public string Artist = "Matthew Whited";
        [Offset(0x16B), Length(54)]
        public string Copyright = "Matthew Whited 2021";

        [Offset(0x237)]
        public MenuValues SelectedShootingMenu = MenuValues.A;
        [Offset(0x238), Length(296)]
        public ShootingMenuOptions[] ShootingMenus = new ShootingMenuOptions[4];

        [Offset(0x360)]
        public MenuValues SelectedCustomMenu = MenuValues.A;

    }

    public enum MenuValues : byte
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
    }

    [Length(68)]
    public class CustomMenu
    {

    }

    [Length(74)]
    public class ShootingMenuOptions
    {
        [Offset(0x000), Length(20)]
        public string MenuName = "                    ";

        [Offset(0x014)]
        public byte Reserved0 = 0;

        [Offset(0x15), Length(4)]
        public string FileNaming_sRGB = "DSC_";
        [Offset(0x19), Length(4)]
        public string FileNaming_Adobe = "_DSC";
    }

}