using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FANNCSharp;
using FANNCSharp.Double;
using System.Text.RegularExpressions;

namespace UnderstandFANN
{
    class Program
    {

        static void Main(string[] args)
        {


            string text = System.IO.File.ReadAllText(@"D:\M2\IAM2\Fanned\UnderstandFANN\UnderstandFANN\text.txt").ToString();
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(text);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);

            string textisLetter="";
            for (int i = 0; i < asciiStr.Length; i++)
            {
                if (Char.IsLetter(asciiStr[i]))
                {
                    textisLetter += asciiStr[i];
                }
            }

            Dictionary<char, int> dict = textisLetter.ToLower().GroupBy(c => c)
                             .ToDictionary(gr => gr.Key, gr => gr.Count());

            foreach(var item in dict.Keys)
            {
                Console.WriteLine(item + " : " + dict[item]);
            }

            FannFile ann = new FannFile("fannFile", asciiStr);


        }

    }
}
