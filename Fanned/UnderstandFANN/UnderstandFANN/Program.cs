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

            string[] lang = new string[3] { "Polonais", "English", "Francais" };
            string text = System.IO.File.ReadAllText(@"D:\M2\IAM2\Fanned\UnderstandFANN\UnderstandFANN\text"+lang[2]+".txt").ToString();
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(text);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            string[] lettres = new string[26];

            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();
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

            double[][] inputs =
            {
                new double[]{0.103, 0.016, 0.054, 0.060, 0.113, 0.010, 0.010, 0.048, 0.056, 0.003, 0.010, 0.035,
                    0.014, 0.065, 0.075, 0.013, 0.000, 0.051, 0.083, 0.111, 0.030, 0.008, 0.019, 0.000, 0.016, 0.000 },
                new double[]{0.076, 0.010, 0.022, 0.039, 0.151, 0.013, 0.009, 0.009, 0.081, 0.001, 0.000, 0.058, 
                    0.024, 0.074, 0.061, 0.030, 0.011, 0.069, 0.100, 0.074, 0.059, 0.015, 0.000, 0.009, 0.003, 0.003, },
                new double[]{0.088, 0.016, 0.030, 0.034, 0.089, 0.004, 0.011, 0.023, 0.071, 0.032, 0.030, 0.025,
                    0.047, 0.058, 0.093, 0.040, 0.000, 0.062, 0.044, 0.035, 0.039, 0.002, 0.044, 0.000, 0.037, 0.046 }
            };

            double[][] outputs =
            {
                new double[]{1,0,0},
                new double[]{0,1,0},
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
            
            int sum = dict.Sum(x => x.Value);
            double[] test = new double[26];
            for(int i = 0; i<26;i++)
            {
                int value = 0;
                if(dict.TryGetValue(alpha[i], out value))
                {
                    //Console.WriteLine((double)value / (double)sum +" "+i);
                    test[i] = (double)value / (double)sum;
                    test[i] = Math.Truncate(test[i] * 1000) / 1000;
                }
                else
                {
                    test[i] = 0.0;
                }
                Console.WriteLine(test[i]);
            }
            double res = 0;
            foreach(double d in test)
            {
                res += d;
            }
            Console.WriteLine(res);


            double[] result = network.Run(test);


            Console.WriteLine("Anglais : {0}", result[0]);
            Console.WriteLine("Francais : {0}", result[1]);
            Console.WriteLine("Polonais : {0}", result[2]);

        }

    }
}
