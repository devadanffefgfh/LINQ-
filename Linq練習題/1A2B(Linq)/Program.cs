using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1A2B_Linq_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("歡迎來到1A2B遊戲!");
            Random rand = new Random();
            var ans = Enumerable.Range(0, 10).OrderBy(x => rand.Next(1, 10)).Take(4).ToArray();

            foreach (var item in ans)
            {
                Console.Write($"{item} ");
            }
            Console.Write("\n");



            while (true)
            {

                Console.Write("輸入數字:");
                string input_num = Console.ReadLine();
                var gusess = input_num.Select(c => c - '0').ToArray();
                /*
                foreach (var item in gusess)
                {
                    Console.Write(item);
                }
                */
                int correctCount = gusess.Where((digit, index) => digit == ans[index]).Count();
                int existcount = gusess.Intersect(ans).Count() - correctCount;
                Console.WriteLine($"{correctCount}A{existcount}B");
                if (correctCount == 4)
                {
                    Console.WriteLine("恭喜你猜對了");
                    Console.WriteLine("要繼續玩嗎?(y/n)");
                    string play = Console.ReadLine();
                    if (play == "n")
                    {
                        break;
                    }
                    else
                    {

                        ans = Enumerable.Range(0, 10).OrderBy(x => rand.Next(1, 10)).Take(4).ToArray();
                        foreach (var item in ans)
                        {
                            Console.Write($"{item} ");
                        }
                    }
                }
            }
        }
    }
}
