using System;
using System.IO;
using System.Media;

namespace ByteBeatinterpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new byte[12000 * 10];
            for (var t = 0; t < data.Length; t++) data[t] = (byte)(t * (42 & t >> 10));
            ByteBeat r1 = new(data);
            r1.CreateByteBeat();
        }
    }
}
