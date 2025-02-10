using _16TeamTexTRPG;

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
    public class GameManager
    {
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameManager();

                return instance;
            }
        }

        public Player player;
        public Stage stage;
        public Shop shop;
        public ItemList itemList;
        public MonsterList monsterList;

        private GameManager()
        {
            
        }

        public void Init()
        {
            //아이템 리스트 생성
            itemList = new ItemList();
            monsterList = new MonsterList();

            //새로운 플레이어를 생성
            player = new Player(1, "메타몽", "전사", 10, 5, 100, 1500, 0, 0);
            stage = new Stage();
            shop = new Shop();
        }

        private string name; //    _ 
        //                         ㅣ->  외부에서 직접 호출할 필요 없어서 Private 사용     
        private Job plyaerJob; //  -

        public enum Job
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

        public void MainScreen() //로비창
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("당신의 이름을 적어주세요.");
            string name = Console.ReadLine();

            player.playJob = SelectJob();

            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Thread.Sleep(500);
            Console.WriteLine();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n");

                Console.WriteLine("1. 거울로 내 상태 보기");
                Console.WriteLine("2. 가방 열어 인벤토리 보기");
                Console.WriteLine("3. 돈 쓰러 상점 가기");
                Console.WriteLine("4. 던전 들어가기");
                Console.WriteLine();
                Thread.Sleep(500);

                int input = CommonUtil.CheckInput(1, 4);

                switch (input)
                {
                    case 1:
                        StatusScreen();
                        break;
                    case 2:
                        //InventoryScreen();
                        break;
                    case 3:
                        shop.ShowMain();
                        break;
                    case 4:
                        stage.BattleField();
                        break;
                }
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

        //캐릭터 상태창
        public void StatusScreen()
        {
            Console.Clear();
            Console.WriteLine("상태창");
            Console.WriteLine("이곳은 캐릭터의 정보를 볼 수 있습니다.");
            Console.WriteLine();

            player.StatusDisplay(); //플레이어 스탯시트 표시

            Thread.Sleep(500);

            Console.WriteLine();
            Console.WriteLine("선택창)");
            Console.WriteLine("0) 나가기");
            Console.WriteLine();

            while (true)
            {
                int input = CommonUtil.CheckInput(0, 0);

                if (input == 0)
                {
                    break;
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
