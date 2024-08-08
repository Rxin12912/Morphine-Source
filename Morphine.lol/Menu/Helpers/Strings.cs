using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Morphine.Framework.Helpers
{
    public class Strings
    {
        public static System.Random random = new System.Random();

        public static string RandomString(int leng)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1243567890";
            var chars = new char[leng];
            for (int i = 0; i < leng; i++)
            {
                chars[i] = alphabet[random.Next(alphabet.Length)];
            }
            return new string(chars);
        }
    }
}
