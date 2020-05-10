using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class LZW
    {
        Dictionary<string, byte> table = new Dictionary<string, byte>();
        public void InitialTable()
        {
            for (int i = 0; i < 256; i++)
            {
                table.Add(Convert.ToString((char)i), (byte)i);
            }
        }

    }
}
