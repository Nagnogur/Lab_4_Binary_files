using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class LZW
    {

        public int[] Compress(byte[] input)
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

        public string[] Decompress(int[] input)
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