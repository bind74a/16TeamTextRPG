using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace _16TeamTextRPG
{
    /*
    string name;
    bool dead;
    int level;
    int maxHealth;
    int health;
    int gold;
    int exp;
    int attack;
    int defense; 

    - Lv2 미니언 
    HP 15
    ATK 5
    - Lv3 공허충
    HP 10
    ATK 9
    - Lv5 대포미니언 
    HP 25
    ATK 8
    */
    internal class Stage
    {
            //구현할것 : 전투신 전체의 로직
        static List<Monster> monsters = new List<Monster>();

        public Monster minion = new Monster { name = "미니언", level = 2, health = 15, attack = 5 };
        public Monster hollowWorm = new Monster { name = "공허충", level = 3, health = 10, attack = 9 };
        public Monster cannonMinion = new Monster { name = "대포미니언", level = 5, health = 25, attack = 8 };
        public Monster casterMinio = new Monster { name = "마법사미니언", level = 4, health = 8, attack = 11 };

        static Random Random = new Random();
        public List<Monster> SummonMonster()//몬스터 소환
        {
            monsters.Add(minion);
            monsters.Add(hollowWorm);
            monsters.Add(cannonMinion);
            monsters.Add(casterMinio);

            //1.몬스터 클래스에서 몬스터 정보받고 그정보로 몬스터 랜덤성으로 1~4마리 소환
            List<Monster> list = new List<Monster>();//랜덤 설정된 몬스터를 받는곳

            int monsterObj = Random.Next(1, 5); //몬스터 마리수 결정

            for (int i = 0; i < monsterObj; i++)
            {
                int randomMon = Random.Next(monsters.Count);//리스트 안 객체를 랜덤하게 정하는것
                Console.WriteLine($"몬스터 소환 {monsters[randomMon]}");
                list.Add(monsters[randomMon]);
            }
            return list; //랜덤설정된 몬스터를 list변수에 보낸다
        }

        public void BattleField()
        { 
           //소환됀 객체들을 필드위에 뛰운다 그리고 필드 몬스터 리스트화?
           Console.WriteLine("Battle!!");
           Console.WriteLine();
           List<Monster> list = SummonMonster();// 이리스트를 다른곳에서 써야함
           foreach (Monster monster in list)//소환된 몬스터 목록 보여주는곳
           {
               if (monster.dead == true)
               {
                    Console.ForegroundColor = ConsoleColor.DarkGray;//글자 어두운 회색으로 변경
                    Console.WriteLine($"Lv.{monster.level} {monster.name} Dead ");
                    Console.ResetColor();
                }
               else
               {
                    Console.WriteLine($"Lv.{monster.level} {monster.name} HP {monster.health} ");
               }
           }

           Console.WriteLine();
           Console.WriteLine("[내정보]");
           Player.StatusDisplay();
           Console.WriteLine();
           Console.WriteLine("1. 공격");
           Console.Write("원하시는 행동을 입력해주세요 : ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            continue;

                    }
                }
            }
                
        }
        //2.공격하기 선택창을 추가하여 공격시 전투 계산 메소드 작동
        public void PlayerAttackField()
        {
            int choiceMonster = 1;
            foreach (Monster monster in SummonMonster())
            {
                if (monster.dead == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;//글자 어두운 회색으로 변경
                    Console.WriteLine($"{choiceMonster}. Lv.{monster.level} {monster.name} Dead ");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{choiceMonster}. Lv.{monster.level} {monster.name} HP {monster.health} ");
                }
                choiceMonster++;
            }

            Console.WriteLine($"{choiceMonster}. 뒤로가기");
            Console.Write("공격할 몬스터를 선택해주세요. : ");

            while (true)
            {
                int playerInput = int.Parse(Console.ReadLine());
                List<Monster> attackMonster = SummonMonster();

                int test = attackMonster.Count;//리스트 개체들의 숫자값


                //소환된 몬스터 개체수 만큼 선택지를 늘리기
                //플레이어 선택번호 랑 리스트 번호 연동
                //플레이어 공격 메서드에게 선택한 개체정보 보내고 연산뒤 결과값 받기
            }
        }
            //3.전투가 끝날시 남아있는 현재체력과 현재 스탯표시
    }
}

