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
            byte[] s = File.ReadAllBytes(@"2.txt");
            LZW test = new LZW();
            int[] b = test.Compress(s);
            for (int i = 0; i < b.Length; i++)
            {
                Console.Write(b[i] + " ");
            }
            Console.ReadKey();
        }
    }
}
