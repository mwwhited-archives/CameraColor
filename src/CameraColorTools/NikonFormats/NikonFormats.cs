﻿using MathNet.Numerics.Interpolation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NikonFormats
{
    public class NikonFormats
    {
        public byte[] PictureControl(
            string description,
            byte? sharpening,

            bool customCurve,

            int? contrast,
            int? brightness,
            int? saturation,
            int? hue,
            IEnumerable<(int x, int y)> points
            )
        {
            if (sharpening < 0 && sharpening > 9) throw new IndexOutOfRangeException(nameof(sharpening));
            if (contrast < -3 && contrast > 3) throw new IndexOutOfRangeException(nameof(contrast));
            if (brightness < -1 && brightness > 1) throw new IndexOutOfRangeException(nameof(brightness));
            if (saturation < -3 && saturation > 3) throw new IndexOutOfRangeException(nameof(saturation));
            if (hue < -3 && hue > 3) throw new IndexOutOfRangeException(nameof(hue));

            var header = new byte[]
            {
                0x4e,0x43,0x50, //NPC + 3
                0,0,0,0, // 3+4
                1,0,0,0, //3+4+4 
                0x24,0x30,0x31,0x30,0x30, //$0100, 3+4+4+5

                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //19 ascii bytes + terminator \0 for description 3+4+4+5+20
                 
                 0x03,0xc2, //Unknown? Address 0x24                
                // 0x06,0x4d, //B&W? Address 0x24

                /*
                 * 00 01 <= Standard
                 * 03 c2 <= Neutral
                 * 00 c3 <= Vivid
                 * 06 4D <= B&W
                 * 04 86 <= Portrait
                 * 04 C7 <= Landscape
                 */

                0x00,0xff, // Unknown

                (byte)(sharpening.HasValue ? 0x80 + Math.Clamp( sharpening.Value & 0x0f, 0, 9) : 0x00), // 0 == Auto, 0x8L Low nibble 0-9 sharpening
                
                (byte)(customCurve ? 0x01 : contrast.HasValue ? 0x80 + Math.Clamp(contrast.Value & 0x0f, -3, 3) : 0x00), //contrast 0x01 on custom curve
                (byte)(customCurve ? 0x01 : brightness.HasValue ? 0x80 + Math.Clamp(brightness.Value & 0x0f, -1, 1) : 0x00), //brightness 0x01 on custom curve
                
                (byte)(saturation.HasValue ? 0x80 + Math.Clamp(saturation.Value & 0x0f, -3, 3) : 0x00), //saturation
                (byte)(hue.HasValue ? 0x80 + Math.Clamp(hue.Value & 0x0f, -3, 3) : 0x00), //hue

                //Color ...
                0xff,0xff,0xff,  // unknown   Address 0x2d 

                //0x82,0x80,0x84,  // unknown   Address 0x2d 
                /*
                 * 0xff,0xff,0xff, <= color
                 * 
                 * For B&W 
                 * Filter Effects           => Off, Yellow, Orange, Red, Green
                 * Toning                   => B&W, Sepia, Cyanotype, Red, Yellow, Green, BlueGreen, Blue, Purple Blue, Red Purple 
                 * Saturation Adjustment    => 1-7
                 * 0x80,0x80,0x84, <= B&W           
                 * 0x80,0x81,0x84, <= sepia         
                 * 0x80,0x82,0x84, <= cyanotype     
                 * 0x80,0x83,0x84, <= red           
                 * 0x80,0x84,0x84, <= yellow        
                 * 0x80,0x85,0x84, <= green         
                 * 0x80,0x86,0x84, <= blue green    
                 * 0x80,0x87,0x84, <= blue          
                 * 0x80,0x88,0x84, <= purple blue   
                 * 0x80,0x89,0x84, <= red purple    
                 */

                0x00,0x00,0x00,  //RESERVED?  Address 0x30
                
                /*
                ////B&W ...
                //0x80,0x08,0xff,  // unknown   Address 0x2d
                //0x00,0x00,0x00,  //RESERVED?  Address 0x30
                */

                (byte)(customCurve ? 0x02 : 0x00), //0x02 = custom curve - 0x00 no custom curve

                0x00,0x00,0x02,0x42,0x49,0x30, //RESERVED?

                0x00,0xFF, //Tone Min - Max               
                0x00,0xFF, //Value min - Max
                0x01,0x00, // XX.YY Fixed decimal factor .05 to 6

                0x00, //curve points count <=====
                
                0x00, 0x00, //Tone, Value - 00
                0x00, 0x00, //Tone, Value - 01
                0x00, 0x00, //Tone, Value - 02
                0x00, 0x00, //Tone, Value - 03
                0x00, 0x00, //Tone, Value - 04
                0x00, 0x00, //Tone, Value - 05
                0x00, 0x00, //Tone, Value - 06
                0x00, 0x00, //Tone, Value - 07
                0x00, 0x00, //Tone, Value - 08
                0x00, 0x00, //Tone, Value - 09
                0x00, 0x00, //Tone, Value - 10
                0x00, 0x00, //Tone, Value - 11
                0x00, 0x00, //Tone, Value - 12
                0x00, 0x00, //Tone, Value - 13
                0x00, 0x00, //Tone, Value - 14
                0x00, 0x00, //Tone, Value - 15
                0x00, 0x00, //Tone, Value - 16
                0x00, 0x00, //Tone, Value - 17
                0x00, 0x00, //Tone, Value - 18
                0x00, 0x00, //Tone, Value - 19

                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //padding
                
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00,

                0x00, 0x00, 0x00, 0x00, //tail
            };

            Array.Copy(Encoding.ASCII.GetBytes(description), 0, header, 16, description.Length);

            var pointData = points.Select(i => new { x = (byte)(i.x & 0xff), y = (byte)(i.y & 0xff) }).Take(20).ToArray();
            var pointBytes = pointData.SelectMany(i => new[] { i.x, i.y }).ToArray();
            header[0x40] = (byte)((pointData.Length) & 0xff);
            Array.Copy(pointBytes, 0, header, 0x41, pointBytes.Length);

            var xs = pointData.Select(i => (double)i.x).ToArray();
            var ys = pointData.Select(i => (double)i.y).ToArray();
            //var spline = CubicSpline.InterpolateAkimaSorted(xs, ys);
            //var spline = CubicSpline.InterpolateNaturalSorted(xs, ys);
            var spline = CubicSpline.InterpolatePchipSorted(xs, ys);
            var interpolated = from x in Enumerable.Range(0, 257)
                               let y = (short)Math.Ceiling(spline.Interpolate(x) / 255 * ((2 << 14) - 1))
                               from b in new[] { (y & 0xff00) >> 8, y & 0xff }
                               select (byte)b;
            var data = interpolated.ToArray();
            Array.Copy(data, 0, header, 0x78, data.Length);

            return header;
        }
    }
}