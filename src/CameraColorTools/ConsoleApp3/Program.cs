﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var rgbs = new[]
            {
                (240,248,255),
                //(250,235,215),
                //(0,255,255),
                //(127,255,212),
                //(240,255,255),
                //(245,245,220),
                //(255,228,196),
                //(0,0,0),
                //(255,235,205),
                //(0,0,255),
                //(138,43,226),
                //(165,42,42),
                //(222,184,135),
                //(95,158,160),
                //(127,255,0),
                //(210,105,30),
                //(255,127,80),
                //(100,149,237),
                //(255,248,220),
                //(220,20,60),
                //(0,255,255),
                //(0,0,139),
                //(0,139,139),
                //(184,134,187),
                //(169,169,169),
                //(0,100,0),
                //(189,183,107),
                //(139,0,139),
                //(85,107,47),
                //(255,140,0),
                //(153,50,204),
                //(139,0,0),
                //(233,150,122),
                //(143,188,139),
                //(72,61,139),
                //(47,79,79),
                //(0,206,209),
                //(148,0,211),
                //(255,20,147),
                //(0,191,255),
                //(105,105,105),
                //(30,144,255),
                //(178,34,34),
                //(255,250,240),
                //(34,139,34),
                //(255,0,255),
                //(220,220,220),
                //(248,248,255),
                //(255,215,0),
                //(218,165,32),
                //(128,128,128),
                //(0,128,0),
                //(173,255,47),
                //(240,255,240),
                //(255,105,180),
                //(205,92,92),
                //(75,0,130),
                //(255,255,240),
                //(240,230,140),
                //(230,230,250),
                //(255,240,245),
                //(124,252,0),
                //(255,250,205),
                //(173,216,230),
                //(240,128,128),
                //(224,255,255),
                //(250,250,210),
                //(211,211,211),
                //(144,238,144),
                //(255,182,193),
                //(255,160,122),
                //(32,178,170),
                //(135,206,250),
                //(119,136,153),
                //(176,196,222),
                //(255,255,224),
                //(0,255,0),
                //(50,205,50),
                //(250,240,230),
                //(255,0,255),
                //(128,0,0),
                //(102,205,170),
                //(0,0,205),
                //(186,85,211),
                //(147,112,219),
                //(60,179,113),
                //(123,104,238),
                //(0,250,154),
                //(72,209,204),
                //(199,21,133),
                //(25,25,112),
                //(245,255,250),
                //(255,228,225),
                //(255,228,181),
                //(255,222,173),
                //(0,0,128),
                //(253,245,230),
                //(128,128,0),
                //(107,142,35),
                //(255,165,0),
                //(255,69,0),
                //(218,112,214),
                //(238,232,170),
                //(152,251,152),
                //(175,238,238),
                //(219,112,147),
                //(255,239,213),
                //(255,218,185),
                //(205,133,63),
                //(255,192,203),
                //(221,160,221),
                //(176,224,230),
                //(128,0,128),
                //(255,0,0),
                //(188,143,143),
                //(65,105,225),
                //(139,69,19),
                //(250,128,114),
                //(244,164,96),
                //(46,139,87),
                //(255,245,238),
                //(160,82,45),
                //(192,192,192),
                //(135,206,235),
                //(106,90,205),
                //(112,128,144),
                //(255,250,250),
                //(0,255,127),
                //(70,130,180),
                //(210,180,140),
                //(0,128,128),
                //(216,191,216),
                //(255,99,71),
                //(64,224,208),
                //(238,130,238),
                //(245,222,179),
                //(255,255,255),
                //(245,245,245),
                //(255,255,0),
                //(154,205,50),
            };

            foreach (var rgb in rgbs)
            {
                var hsv = ColorConversion.Rgb2Hsv(rgb, 255);
                var hsl = ColorConversion.Rgb2Hsl(rgb, 255);

                var hsv2 = ColorConversion.Hsl2Hsv(hsl);
                var hsl2 = ColorConversion.Hsv2Hsl(hsv2);

                var colorRgb = new { r = rgb.Item1.ToString("X2"), g = rgb.Item2.ToString("X2"), b = rgb.Item3.ToString("X2") };
                var colorHsv = new { h = hsv.hue.ToString("0.00"), s = (hsv.saturation * 100.0).ToString("0.00"), v = (hsv.value * 100.0).ToString("0.00") };
                var colorHsl = new { h = hsl.hue.ToString("0.00"), s = (hsl.saturation * 100.0).ToString("0.00"), l = (hsl.lightness * 100.0).ToString("0.00") };

                var colorHsv2 = new { h = hsv2.hue.ToString("0.00"), s = (hsv2.saturation * 100.0).ToString("0.00"), v = (hsv2.value * 100.0).ToString("0.00") };
                var colorHsl2 = new { h = hsl2.hue.ToString("0.00"), s = (hsl2.saturation * 100.0).ToString("0.00"), l = (hsl2.lightness * 100.0).ToString("0.00") };

                var rgbP = ColorConversion.Hsv2Rgb(hsv, 255);
                var colorRgbP = new { r = ((int)rgbP.Item1).ToString("X2"), g = ((int)rgbP.Item2).ToString("X2"), b = ((int)rgbP.Item3).ToString("X2") };

                //Console.WriteLine($"{colorRgb} => {colorHsv} | {colorHsl} => {colorRgbP}");
                Console.WriteLine($"{colorHsv} = {colorHsl} => {colorHsv2} | {colorHsl2}");
            }
        }

    }
}
