using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16TeamTexTRPG
{
    public class CommonUtil
    {
        public static int CheckInput(int min, int max)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input >= min && input <= max)
                        return input;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public static void WriteLine(string value, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
