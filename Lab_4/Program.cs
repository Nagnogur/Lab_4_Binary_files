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
            byte[] s = File.ReadAllBytes(@"1.txt");
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]);
            }
            
                Console.ReadKey();
        }
    }
}
