using System;

namespace NikonFormats
{
    public class LengthAttribute : Attribute
    {
        public LengthAttribute(int length) => Length = length;

        public int Length { get; }
    }

}