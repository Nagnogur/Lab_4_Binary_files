using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class LZW
    {
        Dictionary<string, int> table = new Dictionary<string, int>();
        public int[] Compress(byte[] input)
        {
            for (int i = 0; i < 256; i++)
            {
                table.Add(Convert.ToString((char)i), i);
            }

            string s = "" + (char)input[0];
            string c = "";
            int k = 256;
            int len = input.Length;
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
            
            return res.ToArray();;
        }
    }
}
