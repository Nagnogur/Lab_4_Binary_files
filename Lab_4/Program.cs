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
            if (Console.ReadLine() == "compress")
            {
                int n = Convert.ToInt32(Console.ReadLine());
                string output = Console.ReadLine();
                List<string> files = new List<string>();
                for (int i = 0; i < n; i++)
                {
                    files.Add(Console.ReadLine());
                }
                LZW archive = new LZW(files, output);
                archive.Compress();
            }
            else
            {
                string file = Console.ReadLine();
                LZW archive = new LZW(file);
                archive.Decompress();
            }

            /*byte[] s = File.ReadAllBytes(@"mouse.bmp");
            LZW test = new LZW();
            int[] b = test.Compress(s);
            byte[] bytes = new byte[b.Length * 2];
            for (int i = 0; i < b.Length; i++)
            {
                bytes[i * 2] = (byte)(b[i] & 0xFF);
                bytes[i * 2 + 1] = (byte)((b[i] >> 8) & 0xFF);
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(@"mouse.LZW", FileMode.Create)))
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    writer.Write(bytes[i]);
                }
            }

            byte[] s1 = File.ReadAllBytes(@"mouse.LZW");
            int[] bb = new int[s1.Length / 2];
            for (int i = 0; i < bb.Length; i++)
            {
                bb[i] = s1[i * 2] | s1[i * 2 + 1] << 8;
            }

            for (int i = 0; i < bb.Length; i++)
            {
                Console.Write(bb[i] + " ");
            }
            string[] dec = test.Decompress(bb);
            List<byte> decompress = new List<byte>();
            for (int i = 0; i < dec.Length; i++)
            {
                for (int j = 0; j < dec[i].Length; j++)
                {
                    decompress.Add((byte)dec[i][j]);
                }
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(@"mouse_dec.bmp", FileMode.Create)))
            {
                for (int i = 0; i < decompress.Count; i++)
                {
                    writer.Write(decompress[i]);
                }
            }*/
            Console.ReadKey();
        }
    }
}