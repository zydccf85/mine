using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManager.Common
{
    class GeneratorID
    {
        public static int generateYear()
        {
            StringBuilder bu = new StringBuilder();
            Random rd = new Random();
            int year = rd.Next(1900, DateTime.Now.Year);
            bu.Append(year);
            return int.Parse(bu.ToString());
        }
        //生成
        public static string generate()
        {
            StringBuilder bu = new StringBuilder();
            Random rd = new Random();
            bu.Append(110000);
            int year = generateYear();
            bu.Append(year);
            int month = rd.Next(1, 12);
            if (month < 10)
            {
                bu.Append(0);
            }
            bu.Append(month);
            int[] days;
            if (!isleapyear(year))
            {
                days = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            }
            else
            {
                days = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            }

            int day = rd.Next(1, days[month]);
            if (day < 10)
            {
                bu.Append(0);
            }
            bu.Append(day);
            bu.Append(randomcode());
            int[] c = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            char[] r = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            string convert = bu.ToString();

            int it = 0;
            int res = 0;
            //  Console.WriteLine(res);

            while (it < 16)
            {
                Debug.WriteLine(convert[it]);
                res = res + (convert[it] - '0') * c[it];
                  Debug.WriteLine("第"+it+"次,"+convert[it]+"乘以"+c[it]);
                  Debug.WriteLine(res);
                it++;
            }


            int i = res % 11;
            bu.Append(r[i]);
            return bu.ToString();
        }
        //生成3位随机
        public static string randomcode()
        {
            StringBuilder bu = new StringBuilder();
            Random rd = new Random();
            int code = rd.Next(1, 999);
            if (code < 10)
            {
                bu.Append(00);
            }
            else if (code < 100)
            {
                bu.Append(0);
            }
            bu.Append(code);
            return bu.ToString();
        }


        public static bool isleapyear(int yN)
        {
            if ((yN % 400 == 0 && yN % 3200 != 0)
             || (yN % 4 == 0 && yN % 100 != 0)
            || (yN % 3200 == 0 && yN % 172800 == 0))
                 return true;
                         else
                 return false;
        }
    }
}
