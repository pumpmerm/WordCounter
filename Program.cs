using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordcounterCon
{
    class Program
    {
        static void Main(string[] args)
        {

            // 字符串
            string text;
            
            // 读取文件，存入字符串
            // text = @"You have two variables: i is a value type variable, and o is a reference type variable. How does it make sense to assign the value of i to o?";

            try
            {
                text=File.ReadAllText("../../word.txt");
            }
            catch(IOException e)
            {
                Console.WriteLine("An IO exception has been thrown!");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
                return;
            }

            string [] S=WordFilter(wordPrePro(text),2);

            int[] C;
            string [] S1=WordProcesser(S, out C);

            for (int i = 0; i < S1.Length; i++)
            {
                Console.WriteLine("{0,-15}{1,5}", S1[i], C[i]);
            }

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"../../wordoutput.txt"))
            {
                for (int i = 0; i < S1.Length; i++)
                {
                   file.WriteLine("{0,-15}{1,5}", S1[i], C[i]);
                }
            }
            Console.Read();
        }

       static string[] wordPrePro(string oriString)
        {
            char[] c = { ' ', ':', ',', '.', '?' };
            string[] oriWord = oriString.Split(c, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < oriWord.Length; i++)
            {
                oriWord[i] = (oriWord[i].Trim()).ToLower();
            }
            return oriWord;
        }

        static string[] WordFilter(string[] oriString, int n)
        {
            string[] tempString = new string[oriString.Length];
            int ti = 0;

            for (int i = 0; i < oriString.Length; i++)
            {
                if (oriString[i].Length > n)
                {
                    tempString[ti] = oriString[i];
                    ti++;
                }
            }

            Array.Resize<string>(ref tempString, ti);
            return tempString;
        }
        static string[] WordProcesser(string[] oriWord, out int[] counter)
        {
            // 比较字符串
            counter = new int[oriWord.Length];

            // 临时字符串数组，存储比较过程中的字符串
            string[] tempWord = new string[oriWord.Length];
            int wordNumber = oriWord.Length;

            for (int i = 0; i < oriWord.Length - 1; i++)
            {
                tempWord[i] = oriWord[i];
                int ti = i + 1;
                counter[i] = 1;

                for (int j = i + 1; j < oriWord.Length; j++)
                {
                    if (tempWord[i] != oriWord[j])
                    {
                        tempWord[ti] = oriWord[j];
                        ti++;
                    }
                    else
                    {
                        wordNumber--;
                        counter[i]++;
                    }
                }

                Array.Resize<string>(ref oriWord, wordNumber);
                Array.Resize<string>(ref tempWord, wordNumber);
                oriWord = (string[])tempWord.Clone();//数组尾部有剩余元素，应该首先将oriWord清空
            }

            counter[wordNumber - 1] = 1;
            return oriWord;
        }
    }
}
