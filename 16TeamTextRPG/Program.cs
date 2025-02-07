namespace _16TeamTextRPG
{
    //게임 매니저 클래스
    class GameManager
    {
        Player player;

        public GameManager()
        {
            //새로운 플레이어를 생성
            player = new Player(1, "메타몽", "전사", 10, 5, 100, 1500);

            //아이템 리스트 생성
        }

        private string name; //    _ 
        //                         ㅣ->  외부에서 직접 호출할 필요 없어서 Private 사용     
        private Job plyaerJob; //  -

        enum Job
        {
            Warrior = 1, // 1을 안쓰면 0부터 시작하기에 1부터 시작하도록 값을 부여
            Mage,
            Archer,
            Thief,
        }

        private Dictionary<Job, string> jobNames = new Dictionary<Job, string>() //enum에 제시된 영어를 한글화
        {
            {Job.Warrior, "전사"},
            {Job.Mage, "마법사"},
            {Job.Archer, "궁수"},
            {Job.Thief, "도적"}
        };

        public void MainScreen()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("당신의 이름을 적어주세요.");
            string name = Console.ReadLine();

            playJob = SelcetJob()

            Console.WriteLine();
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 아처");
            Console.WriteLine("4. 마법사");
            string job = Console.ReadLine();    
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine("1. 거울로 내 상태 보기");
            Console.WriteLine("2. 가방 열어 인벤토리 보기");
            Console.WriteLine("3. 돈 쓰러 상점 가기");
            Console.WriteLine("4. 던전 들어가기");
            Console.WriteLine();
            Thread.Sleep(500);

            //숫자를 입력하면 해당하는 화면으로 이동
            while (true)
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

        private Job SelectJob()
        {
            while (true)
            {
                Console.WriteLine("\n하고 싶은 직업을 골라주세요.");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 마법사");
                Console.WriteLine("3. 궁수");
                Console.WriteLine("4. 도적");
                Console.Write("번호 입력해주세요\n>>");

                if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(Job), choice))
                {
                    return (Job)choice;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 1~4 사이의 숫자를 입력하세요.");
                }

            }
        }

        //캐릭터 선택창
        public void StatusScreen()
        {
            Console.Clear();
            Console.WriteLine("【상태창】");
            Console.WriteLine("이곳은 캐릭터의 정보를 볼 수 있습니다.");
            Console.WriteLine();
            Thread.Sleep(500);

            player.StatusDisplay(); //플레이어 스탯시트 표시


            Thread.Sleep(500);
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
                    MainScene();
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
