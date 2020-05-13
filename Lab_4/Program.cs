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
            /*switch(args[0])
            {
                case "compress":
                    {
                        List<string> files = new List<string>();
                        for (int i = 2; i < args.Length; i++)
                        {
                            files.Add(args[i]);
                        }
                        LZW archive = new LZW(files);
                        
                        archive.Compress();
                        break;
                    }
            }*/

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

            byte[] s = File.ReadAllBytes(@"story.mp3");
            LZW test = new LZW(files, output);
            int[] b = test.Compress(s);
            for (int i = 0; i < b.Length; i++)
            {
                Console.Write(b[i] + " ");
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(@"1.LZW", FileMode.Create)))
            {
                for (int i = 0; i < b.Length; i++)
                {
                    writer.Write(b[i]);
                }
            }

            string[] dec = test.Decompress(b);
            List<byte> decompress = new List<byte>();
            for (int i = 0; i < dec.Length; i++)
            {
                for (int j = 0; j < dec[i].Length; j++)
                {
                    decompress.Add((byte)dec[i][j]);
                }
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(@"11.mp3", FileMode.Create)))
            {
                for (int i = 0; i < decompress.Count; i++)
                {
                    writer.Write(decompress[i]);
                }
            }
            Console.ReadKey();
        }
    }
}
