namespace _16TeamTextRPG
{
    internal class Program
    {
        //깃허브 테스트 2
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    //게임 매니저 클래스
    class GameManager
    {
        Player player;

        public GameManager()
        {
            //새로운 플레이어를 생성
            player = new Player(1, "메타몽", "전사",  10, 5, 100, 1500);

            //아이템 리스트 생성
        }


        public void MainScreen() //로비창
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine("1. 거울로 내 상태 보기");
            Console.WriteLine("2. 가방 열어 인벤토리 보기");
            Console.WriteLine("3. 돈 쓰러 상점 가기");
            Console.WriteLine("4. 던전 들어가기");
            Console.WriteLine();
            Thread.Sleep(500);

            while (true)    //숫자를 입력하면 해당하는 화면으로 이동
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                Console.ReadLine();

                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        StatusScreen();
                        break;
                    case 2:
                        InventoryScreen();
                        break;
                    case 3:
                        ShopScreen();
                        break;
                    case 4:
                        DunguenScreen();
                        break;
                }

                Console.WriteLine("잘못된 입력입니다. 다시 입력해 주세요.");
            }
        }

        //캐릭터 상태창
        public void StatusScreen()
        {
            Console.Clear();
            Console.WriteLine("상태창");
            Console.WriteLine("이곳은 캐릭터의 정보를 볼 수 있습니다.");
            Console.WriteLine();

            player.StatusDisplay(); //플레이어 스탯시트 표시

            Console.WriteLine();
            Console.WriteLine("선택창)");
            Console.WriteLine("0) 나가기");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                Console.ReadLine();

                int input = int.Parse(Console.ReadLine());

                if (input == 0)
                {
                    MainScreen();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해 주세요.");
                    continue;
                }

            }
        }
    }
}
