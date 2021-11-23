using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siwe.Base36
{
    public static class Base36Converter
    {
        private const int    Base  = 36;
        private const long   LBase = 36;
        private const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ConvertTo(int value)
        {
            string result = "";

            while (value > 0)
            {
                result = Chars[value % Base] + result;
                value /= Base;
            }

            return result;
        }

        public static string ConvertTo(long value)
        {
            int    idx    = 0;
            string result = "";

            while (value > 0)
            {
                idx = (int)(value % LBase);
                
                result = Chars[idx] + result;
                value /= Base;
            }

            return result;
        }
    }
}
