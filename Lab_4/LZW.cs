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
        List<string> files { set; get; }
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
            int fileCount = files.Count;
            int[] len = new int[fileCount * 2 + 1];
            int l = 0;
            foreach (var file in files)
            {
                byte[] fileContent = File.ReadAllBytes(@file);
                byte[] fileName = Encoding.ASCII.GetBytes(file);
                len[l * 2] = fileContent.Length;
                len[l * 2 + 1] = fileName.Length;
                l++;
                bytes.AddRange(fileContent.Concat(fileName).ToArray());
                File.Delete(@file);
            }
            len[fileCount * 2] = fileCount;
            byte[] input = bytes.ToArray();
            int[] o = Compress1(input);
            int[] output = o.Concat(len).ToArray();
            byte[] write = new byte[output.Length * 2];
            for (int i = 0; i < output.Length; i++)
            {
                write[i * 2] = (byte)(output[i] & 0xFF);
                write[i * 2 + 1] = (byte)((output[i] >> 8) & 0xFF);
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(@name, FileMode.Create)))
            {
                for (int i = 0; i < write.Length; i++)
                {
                    writer.Write(write[i]);
                }
            }
        }
        public int[] Compress1(byte[] input)
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

        public int Decompress()
        {
            byte[] input = File.ReadAllBytes(@name);
            int len = input.Length;
            int fileNumber = input[len - 2] | input[len - 1] << 8;
            int[] content = new int[2 * fileNumber];
            int n = len - 2 - 4*fileNumber;
            for (int i = 0; i < fileNumber; i++)
            {
                content[2 * i] = input[n] | input[n + 1] << 8;
                n += 2;
                content[2 * i + 1] = input[n] | input[n + 1] << 8;
                n += 2;
            }
            int[] com = new int[len - 2 - 4 * fileNumber];
            for (int i = 0; i < (len - 2 - 4*fileNumber) / 2; i++)
            {
                com[i] = input[i * 2] | input[i * 2 + 1] << 8;
               // Console.Write(com[i] + " ");
            }
            
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
                names[i] = "";
                for (int j = 0; j < content[2 * i + 1]; j++, n++)
                {
                    names[i] += Convert.ToChar(decompress[n]);
                }
                //n -= content[2 * i + 1];
                Console.WriteLine(names[i]);
            }
            n = 0;
            int sum = 0;
            for (int i = 0; i < fileNumber; i++)
            {
                sum += content[2 * i];
                using (BinaryWriter writer = new BinaryWriter(File.Open(@names[i], FileMode.Create)))
                {
                    for (int j = n; j < sum ; j++)
                    {
                        writer.Write(decompress[j]);
                    }
                }

                n = sum + content[2 * i + 1];
                sum += content[2 * i + 1];
                
            }
            return fileNumber;

        }
        public string[] Decompress1(int[] input)
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