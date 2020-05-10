using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] s = File.ReadAllBytes(@"mouse.bmp");
            LZW test = new LZW();
            int[] b = test.Compress(s);
            for (int i = 0; i < b.Length; i++)
            {
                Console.Write(b[i] + " ");
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(@"mouse.LZW", FileMode.Create)))
            {
                for (int i = 0; i < b.Length; i++)
                {
                    writer.Write(b[i]);
                }
            }
            
            Console.ReadKey();
        }
    }
}
