using System;
using System.Linq;

namespace WpfApp1.Drawings
{
    public class ColorConversion
    {
        public static (double hue, double saturation, double lightness) Rgb2Hsl((byte red, byte green, byte blue) color) => Rgb2Hsl(color.red, color.green, color.blue);
        public static (double hue, double saturation, double lightness) Rgb2Hsl(byte red, byte green, byte blue) => Rgb2Hsl((red, green, blue), 255.0);
        public static (double hue, double saturation, double lightness) Rgb2Hsl((double red, double green, double blue) color, double factor = 1.0)
        {
            var primes = (red: color.red / factor, green: color.green / factor, blue: color.blue / factor);
            var c = (max: primes.ToArray<double>().Max(), min: primes.ToArray<double>().Min());
            var diff = c.max - c.min;
            var lightness = (c.max + c.min) / 2.0;

            return (
                hue: ((diff switch
                {
                    0.0 => 0.0,
                    _ when c.max == primes.red => ((primes.green - primes.blue) / diff) % 6.0,
                    _ when c.max == primes.green => ((primes.blue - primes.red) / diff) + 2.0,
                    _ when c.max == primes.blue => ((primes.red - primes.green) / diff) + 4.0,
                    _ => 0.0
                } * 60) + 360) % 360,
                saturation: diff switch
                {
                    0.0 => 0.0,
                    _ => diff / (1.0 - Math.Abs(2 * lightness - 1))
                },
                lightness: lightness
                );
        }

        public static (double hue, double saturation, double value) Rgb2Hsv((byte red, byte green, byte blue) color) => Rgb2Hsv(color.red, color.green, color.blue);
        public static (double hue, double saturation, double value) Rgb2Hsv(byte red, byte green, byte blue) => Rgb2Hsv((red, green, blue), 255.0);
        public static (double hue, double saturation, double value) Rgb2Hsv((double red, double green, double blue) color, double factor = 1.0)
        {
            var primes = (red: color.red / factor, green: color.green / factor, blue: color.blue / factor);
            var c = (max: primes.ToArray<double>().Max(), min: primes.ToArray<double>().Min());
            var diff = c.max - c.min;

            return (
                hue: ((diff switch
                {
                    0.0 => 0.0,
                    _ when c.max == primes.red => ((primes.green - primes.blue) / diff) % 6,
                    _ when c.max == primes.green => ((primes.blue - primes.red) / diff) + 2,
                    _ when c.max == primes.blue => ((primes.red - primes.green) / diff) + 4,
                    _ => 0.0
                } * 60) + 360) % 360,
                saturation: c.max switch
                {
                    0.0 => 0.0,
                    _ => diff / c.max
                },
                value: c.max
                );
        }

        public static (double red, double green, double blue) Hsv2Rgb((double hue, double saturation, double value) color, double factor = 1.0)
        {
            var adjusted = (hue: color.hue % 360.0, saturation: color.saturation, value: color.value);

            var c = adjusted.value * adjusted.saturation;
            var x = c * (1 - Math.Abs(adjusted.hue / 60 % 2 - 1));
            var m = adjusted.value - c;

            (double red, double green, double blue) rgb = (((int)(adjusted.hue / 60)) % 6) switch
            {
                0 => (c, x, 0),
                1 => (x, c, 0),
                2 => (0, c, x),
                3 => (0, x, c),
                4 => (x, 0, c),
                _ => (c, 0, x)
            };

            return ((rgb.red + m) * factor, (rgb.green + m) * factor, (rgb.blue + m) * factor);
        }
        public static (double red, double green, double blue) Hsl2Rgb((double hue, double saturation, double lightness) color, double factor = 1.0)
        {
            var adjusted = (hue: (color.hue + 360.0) % 360.0, saturation: color.saturation, lightness: color.lightness);

            var c = (1 - Math.Abs(2.0 * adjusted.lightness - 1.0)) * adjusted.saturation; ;
            var x = c * (1 - Math.Abs(adjusted.hue / 60 % 2 - 1));
            var m = adjusted.lightness - c / 2.0;

            (double red, double green, double blue) rgb = (((int)(adjusted.hue / 60)) % 6) switch
            {
                0 => (c, x, 0),
                1 => (x, c, 0),
                2 => (0, c, x),
                3 => (0, x, c),
                4 => (x, 0, c),
                _ => (c, 0, x)
            };

            return ((rgb.red + m) * factor, (rgb.green + m) * factor, (rgb.blue + m) * factor);
        }

        public static (double hue, double saturation, double value) Hsl2Hsv((double hue, double saturation, double lightness) color)
        {
            var adjusted = (hue: (color.hue + 360.0) % 360.0, saturation: color.saturation, lightness: color.lightness);

            var c = (1 - Math.Abs(2.0 * adjusted.lightness - 1.0)) * adjusted.saturation;
            var x = c * (1 - Math.Abs(adjusted.hue / 60 % 2 - 1));
            var m = adjusted.lightness - c / 2.0;

            var terms = new[] { c + m, x + m, m };

            var c2 = (max: terms.Max(), min: terms.Min());
            var diff = c2.max - c2.min;

            return (
                hue: adjusted.hue,
                saturation: c2.max == 0.0 ? 0.0 : diff / c2.max,
                value: c2.max
                );
        }
        public static (double hue, double saturation, double lightness) Hsv2Hsl((double hue, double saturation, double value) color)
        {
            var adjusted = (hue: (color.hue + 360.0) % 360.0, saturation: color.saturation, value: color.value);

            var c = adjusted.value * adjusted.saturation;
            var x = c * (1 - Math.Abs(adjusted.hue / 60 % 2 - 1));
            var m = adjusted.value - c;

            var rgbP = new[] { c + m, x + m, m };
            var c2 = (max: rgbP.Max(), min: rgbP.Min());
            var diff = c2.max - c2.min;
            var lightness = (c2.max + c2.min) / 2.0;

            return (
                hue: adjusted.hue,
                saturation: diff == 0.0 ? 0.0 : diff / (1.0 - Math.Abs(2 * lightness - 1)),
                lightness: lightness
                );
        }

        public static (double red, double yellow, double blue) Rgb2Ryb((double red, double green, double blue) color, double factor = 1.0) => Ryb.rgb2ryb((color.red, color.green, color.blue), factor);
        public static (double red, double green, double blue) Ryb2Rgb((double red, double yellow, double blue) color, double factor = 1.0) => Ryb.ryb2rgb((color.red, color.yellow, color.blue), factor);

        public static (double red, double green, double blue) Cmy2Rgb((double cyan, double magenta, double yellow) color, double factor = 1.0) =>
            (
                red: factor * (1 - color.cyan),
                green: factor * (1 - color.magenta),
                blue: factor * (1 - color.yellow)
            );
        public static (double cyan, double magenta, double yellow) Rgb2Cmy((double red, double green, double blue) color, double factor = 1.0)
        {
            var adjusted = (red: color.red / factor, green: color.green / factor, blue: color.blue / factor);
            return (
                cyan: 1 - adjusted.red,
                magenta: 1 - adjusted.green,
                yellow: 1 - adjusted.blue
                );
        }

        public static (double x, double y, double z) Rgb2Xyz((double red, double green, double blue) color, double factor = 1.0)
        {
            // https://mina86.com/2019/srgb-xyz-conversion/
            // https://www.image-engineering.de/library/technotes/958-how-to-convert-between-srgb-and-ciexyz
            // http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html
            // http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html

            var primes = (red: color.red / factor, green: color.green / factor, blue: color.blue / factor);
            return primes;
            //var primes = (red: color.red / factor, green: color.green / factor, blue: color.blue / factor);
            //var c = (max: primes.ToArray<double>().Max(), min: primes.ToArray<double>().Min());
            //var diff = c.max - c.min;
            //var lightness = (c.max + c.min) / 2.0;

            //return (
            //    hue: ((diff switch
            //    {
            //        0.0 => 0.0,
            //        _ when c.max == primes.red => ((primes.green - primes.blue) / diff) % 6.0,
            //        _ when c.max == primes.green => ((primes.blue - primes.red) / diff) + 2.0,
            //        _ when c.max == primes.blue => ((primes.red - primes.green) / diff) + 4.0,
            //        _ => 0.0
            //    } * 60) + 360) % 360,
            //    saturation: diff switch
            //    {
            //        0.0 => 0.0,
            //        _ => diff / (1.0 - Math.Abs(2 * lightness - 1))
            //    },
            //    lightness: lightness
            //    );
        }
        public static (double red, double green, double blue) Xyz2Rgb((double x, double y, double z)color, double factor = 1.0)
        {
            var primes = ( color.x * factor,  color.y * factor,  color.z * factor);
            return primes;
        }



        public static (double red, double green, double blue) Cmyk2Rgb((double cyan, double magenta, double yellow, double black) color, double factor = 1.0) =>
            (
                red: factor * (1 - color.cyan) * (1 - color.black),
                green: factor * (1 - color.magenta) * (1 - color.black),
                blue: factor * (1 - color.yellow) * (1 - color.black)
            );
        public static (double cyan, double magenta, double yellow, double black) Rgb2Cmyk((double red, double green, double blue) color, double factor = 1.0)
        {
            var adjusted = (red: color.red / factor, green: color.green / factor, blue: color.blue / factor);
            var black = 1 - adjusted.ToArray<double>().Max();
            return (
                cyan: black == 1.0 ? (1 - adjusted.red - black) : (1 - adjusted.red - black) / (1 - black),
                magenta: black == 1.0 ? (1 - adjusted.green - black) : (1 - adjusted.green - black) / (1 - black),
                yellow: black == 1.0 ? (1 - adjusted.blue - black) : (1 - adjusted.blue - black) / (1 - black),
                black: black
                );
        }

        public class Ryb
        {
            private static double cubic(double t, double A, double B) => A + t * t * (3 - 2 * t) * (B - A);
            private static double getR(double red, double yellow, double blue) =>
                cubic(red,
                    cubic(yellow, cubic(blue, 1.0, 0.163), cubic(blue, 1.0, 0.000)),
                    cubic(yellow, cubic(blue, 1.0, 0.500), cubic(blue, 1.0, 0.200))
                    );
            private static double getG(double red, double yellow, double blue) =>
                cubic(red,
                    cubic(yellow, cubic(blue, 1.0, 0.373), cubic(blue, 1.0, 0.660)),
                    cubic(yellow, cubic(blue, 0.0, 0.000), cubic(blue, 0.5, 0.094))
                    );
            private static double getB(double red, double yellow, double blue) =>
                cubic(red,
                    cubic(yellow, cubic(blue, 1.0, 0.600), cubic(blue, 0.0, 0.200)),
                    cubic(yellow, cubic(blue, 0.0, 0.500), cubic(blue, 0.0, 0.000))
                    );
            public static (double red, double green, double blue) ryb2rgb((double red, double yellow, double blue) color, double factor = 1.0) =>
                (
                red: getR(color.red, color.yellow, color.blue) * factor,
                green: getG(color.red, color.yellow, color.blue) * factor,
                blue: getB(color.red, color.yellow, color.blue) * factor
                );

            private static double _getR(double red, double green, double blue) =>
                cubic(red,
                    cubic(green, cubic(blue, 1.0, 0.000), cubic(blue, 1.0, 0.0)),
                    cubic(green, cubic(blue, 1.0, 0.309), cubic(blue, 0.0, 0.0))
                    );
            private static double _getY(double red, double green, double blue) =>
                cubic(red,
                    cubic(green, cubic(blue, 1.0, 0.000), cubic(blue, 1.0, 0.053)),
                    cubic(green, cubic(blue, 0.0, 0.000), cubic(blue, 1.0, 0.094))
                    );
            private static double _getB(double red, double green, double blue) =>
                cubic(red,
                    cubic(green, cubic(blue, 0.0, 1.000), cubic(blue, 0.483, 0.210)),
                    cubic(green, cubic(blue, 0.0, 0.469), cubic(blue, 0.000, 0.000))
                    );
            public static (double red, double yellow, double blue) rgb2ryb((double red, double green, double blue) color, double factor = 1.0)
            {
                var adjusted = (red: color.red / factor, green: color.green / factor, blue: color.blue / factor);

                return (
                red: getR(adjusted.red, adjusted.green, adjusted.blue) * factor,
                yellow: getG(adjusted.red, adjusted.green, adjusted.blue) * factor,
                blue: getB(adjusted.red, adjusted.green, adjusted.blue) * factor
                );
            }
        }
    }
}
