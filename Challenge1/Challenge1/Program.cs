using System;
using System.Text;

namespace Challenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("Challenge3WordList.txt");
            string[] sortedLines = new string[lines.Length];

            for(int i = 0; i < lines.Length; i++)
            {
                sortedLines[i] = SortWord(lines[i]);
                //Console.WriteLine(sortedLines[i]);
            }

            string[] words = new string[] { "mkeart", "sleewa", "edcudls", "iragoge", "usrlsle", "nalraoci", "nsdeuto", "amrhat", "inknsy","iferkna" };

            foreach(string word in words)
            {
                string sortedWord = SortWord(word);
                for(int i = 0; i < lines.Length; i++)
                {
                    if (sortedWord.Equals(sortedLines[i]))
                    {
                        Console.WriteLine("{0} unscrambled is {1}.", word, lines[i]);
                    }
                }
            }



            Console.ReadLine();
        }

        static string SortWord(string s)
        {
            char[] ca = s.ToCharArray();
            Array.Sort(ca);
            StringBuilder sb = new StringBuilder();
            foreach (char c in ca) sb.Append(c);
            return sb.ToString();
        }
    }
}
