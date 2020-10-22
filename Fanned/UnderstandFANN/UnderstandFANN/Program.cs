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

        
            double[][] inputs =
            {
                GetFrequencies("appEnglish1"),
                GetFrequencies("appEnglish2"),
                GetFrequencies("appEnglish3"),
                GetFrequencies("appFrancais1"),
                GetFrequencies("appFrancais2"),
                GetFrequencies("appFrancais3"),
                GetFrequencies("appPolonais1"),
                GetFrequencies("appPolonais2"),
                GetFrequencies("appPolonais3")
            };

            double[][] outputs =
            {
                new double[]{1,0,0},
                new double[]{1,0,0},
                new double[]{1,0,0},
                new double[]{0,1,0},
                new double[]{0,1,0},
                new double[]{0,1,0},
                new double[]{0,0,1},
                new double[]{0,0,1},
                new double[]{0,0,1}
            };

            List<uint> layers = new List<uint>();
            layers.Add(26);
            layers.Add(4);
            layers.Add(3);

            NeuralNet network = new NeuralNet(FANNCSharp.NetworkType.LAYER, layers);

            TrainingData data = new TrainingData();
            data.SetTrainData(inputs, outputs);

            network.TrainOnData(data, 3000, 100, 0.001f);

            Console.WriteLine("Final Error :" + network.MSE);



             
            double[] test = GetFrequencies("textEnglish");
            double[] result = network.Run(test);


            Console.WriteLine("Anglais : {0}", result[0]);
            Console.WriteLine("Francais : {0}", result[1]);
            Console.WriteLine("Polonais : {0}", result[2]);

        }


        static double[] GetFrequencies(string txtFilename)
        {
            string text = System.IO.File.ReadAllText(@"..\texts\" + txtFilename + ".txt").ToString();
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(text);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            string[] lettres = new string[26];

            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();
            string textisLetter = "";
            for (int i = 0; i < asciiStr.Length; i++)
            {
                if (Char.IsLetter(asciiStr[i]))
                {
                    textisLetter += asciiStr[i];
                }
            }

            Dictionary<char, int> dict = textisLetter.ToLower().GroupBy(c => c)
                             .ToDictionary(gr => gr.Key, gr => gr.Count());

            foreach (var item in dict.Keys)
            {
                Console.WriteLine(item + " : " + dict[item]);
            }
            int sum = dict.Sum(x => x.Value);
            double[] freq = new double[26];
            for (int i = 0; i < 26; i++)
            {
                int value = 0;
                if (dict.TryGetValue(alpha[i], out value))
                {
                    //Console.WriteLine((double)value / (double)sum +" "+i);
                    freq[i] = (double)value / (double)sum;
                    freq[i] = Math.Truncate(freq[i] * 1000) / 1000;
                }
                else
                {
                    freq[i] = 0.0;
                }
                Console.WriteLine(freq[i]);
            }
            double res = 0;
            foreach (double d in freq)
            {
                res += d;
            }
            Console.WriteLine(res);
            return freq;
        }



    }
}
