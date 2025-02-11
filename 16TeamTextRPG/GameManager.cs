using _16TeamTexTRPG;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16TeamTextRPG
{
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
        public Inventory inventory;
        public Guild guild;

        private GameManager()
        {

        }

        public void Init()
        {
            //아이템 리스트 생성
            itemList = new ItemList();
            monsterList = new MonsterList();

            //새로운 플레이어를 생성
            player = new Player(1, name, "", 10, 5, 100, 1500, 0, 10);
            stage = new Stage();
            shop = new Shop();
            inventory = new Inventory();
            guild = new Guild();
        }

        public void Free()
        {
            player.Free();
            inventory.Free();
        }

        private string name; //    _ 
                                //                         ㅣ->  외부에서 직접 호출할 필요 없어서 Private 사용     
        private Job playerJob; //  -

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

        //직업별 능력치 창
        private void SetPlayerStats(Player player)
        {
            switch (player.playJob)
            {
                case GameManager.Job.Warrior:
                    player.atk = 5;
                    player.def = 10;
                    player.maxHp = 120;
                    break;
                case GameManager.Job.Mage:
                    player.atk = 15;
                    player.def = 2;
                    player.maxHp = 80;
                    break;
                case GameManager.Job.Archer:
                    player.atk = 12;
                    player.def = 5;
                    player.maxHp = 80;
                    break;
                case GameManager.Job.Thief:
                    player.atk = 10;
                    player.def = 5;
                    player.maxHp = 100;
                    break;
            }

            // 초기 HP 설정
            player.hp = player.maxHp;
        }

        public void MainScreen() //로비창
        {
            Console.Clear();
            if (player.Load()) // 플레이어 불러오기 성공했을때
            {
                inventory.LoadInven();
            }
            else // 세이브파일이 없을때
            {
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("당신의 이름을 적어주세요.");
                string name = Console.ReadLine();

                player.name = name;
                player.playJob = SelectJob();
                player.job = jobNames[player.playJob]; //직업 선택시 상태창에 표기가 되지 않아 추가 
                SetPlayerStats(player);

                Console.WriteLine("이제 전투를 시작할 수 있습니다.");
                Thread.Sleep(500);
                Console.WriteLine();
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n");

                Console.WriteLine("1. 거울로 내 상태 보기");
                Console.WriteLine("2. 가방 열어 인벤토리 보기");
                Console.WriteLine("3. 돈 쓰러 상점 가기");
                Console.WriteLine($"4. 던전 들어가기 (현재 진행: {stage.floor}층)");
                Console.WriteLine("5. 퀘스트 보기");
                Console.WriteLine("6. 회복 아이템 사용");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Thread.Sleep(500);

                int input = CommonUtil.CheckInput(0, 6);

                if (input == 0)
                {
                    Free();
                    break;
                }

                switch (input)
                {
                    case 1:
                        StatusScreen();
                        break;
                    case 2:
                        inventory.InventoryScreen();
                        break;
                    case 3:
                        shop.ShowMain();
                        break;
                    case 4:
                        stage.BattleField();
                        break;
                    case 5:
                        guild.ShowMain();
                        break;
                    case 6:
                        inventory.ConsumableScreen();
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

                int choice = CommonUtil.CheckInput(1, 4);

                switch (choice)
                {
                    case 1:
                        return Job.Warrior;
                    case 2:
                        return Job.Mage;
                    case 3:
                        return Job.Archer;
                    case 4:
                        return Job.Thief;
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
