using _16TeamTexTRPG;
using System.Runtime.CompilerServices;

namespace _16TeamTextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.Init();

            gameManager.MainScreen();
        }
    }

    //게임 매니저 클래스
   
}
