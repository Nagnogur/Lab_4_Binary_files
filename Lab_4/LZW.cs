using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class LZW
    {
        List <string> files { set; get; }
        string name { get; set; }
        public LZW(List<string> input, string name)
        {
            files = input;
            this.name = name;
        }
        public LZW(string name)
        {
            this.name = name;
        }
        public void Compress()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(Convert.ToByte(files.Count));
            foreach (var file in files)
            {
                byte[] fileContent = File.ReadAllBytes(@file);
                byte[] fileName = Encoding.ASCII.GetBytes(file);
                bytes.Add(Convert.ToByte(fileContent.Length));
                bytes.Add(Convert.ToByte(fileName.Length));
                bytes.AddRange(fileContent.Concat(fileName).ToArray());
            }
            byte[] input = bytes.ToArray();
            int[] output = Compress1(input);
            using (BinaryWriter writer = new BinaryWriter(File.Open(@name, FileMode.Create)))
            {
                for (int i = 0; i < output.Length; i++)
                {
                    writer.Write(output[i]);
                }
            }
        }

        private int[] Compress1(byte[] input)
        {
            Dictionary<string, int> table = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
            {
                table.Add(Convert.ToString((char)i), i);
            }

            string s = "" + (char)input[0];
            string c = "";
            int k = 256,
                len = input.Length;
            List<int> res = new List<int>();
            for (int i = 0; i < len; i++)
            {
                c = "";
                if (i != len - 1)
                {
                    c = Convert.ToString((char)input[i + 1]);
                }

                if (table.ContainsKey(s + c))
                {
                    s += c;
                }
                else
                {
                    res.Add(table[s]);
                    table.Add(s + c, k);
                    s = c;
                    k++;
                    
                }
            }
            res.Add(table[s]);
            
            return res.ToArray();
        }

        public void Decompress()
        {
            List<int> input = Array.ConvertAll(File.ReadAllBytes(@name), c => (int)c).ToList();
            int fileNumber = input[0];
            input.RemoveAt(0);
            int[] content = new int[2 * fileNumber];
            int n = 0;
            for (int i = 0; i < fileNumber; i++)
            {
                content[2 * i] = input[n];
                content[2 * i + 1] = input[n + 1];
                input.RemoveRange(n, 2);
                n += content[2 * i] + content[2 * i + 1];
            }
            int[] com = input.ToArray();
            string[] dec = Decompress1(com);
            
            List<byte> decompress = new List<byte>();
            for (int i = 0; i < dec.Length; i++)
            {
                for (int j = 0; j < dec[i].Length; j++)
                {
                    decompress.Add((byte)dec[i][j]);
                }
            }
            string[] names = new string[fileNumber];
            n = 0;
            for (int i = 0; i < fileNumber; i++)
            {
                n += content[2 * i];
                for (int j = n; j < content[2 * i + 1]; j++)
                {

                }
            }

            for (int i = 0; i < fileNumber; i++)
            {
                int j = 0;
                using (BinaryWriter writer = new BinaryWriter(File.Open(@"11.mp3", FileMode.Create)))
                {
                    for (int i = 0; i < decompress.Count; i++)
                    {
                        writer.Write(decompress[i]);
                    }
                }
            }

        }

        private string[] Decompress1(int[] input)
        {
            Dictionary<int, string> table = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
            {
                table.Add(i, Convert.ToString((char)i));
            }
            
            int code = input[0];
            int k = 256,
                len = input.Length,
                next;
            List<string> res = new List<string>();
            string s = table[code], c = "";
            res.Add(s);
            for (int i = 0; i < len - 1; i++)
            {
                next = input[i + 1];
                if (!table.ContainsKey(next))
                {
                    s = table[code];
                    s = s + c;
                }
                else
                {
                    s = table[next];
                }

                res.Add(s);
                c = "" + s[0];
                table[k] = table[code] + c;
                k++;
                code = next;
            }
            return res.ToArray();
        }
    }
}
