using _16TeamTexTRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static _16TeamTexTRPG.Skill;


namespace _16TeamTextRPG
{
    public class Stage
    {
        //구현할것 : 전투신 전체의 로직
        public Player player;
        public Skill skill;

        public List<Monster> monsters;

        public List<Skill> skillList;
        //스킬 리스트 변수 생성
        Random Random = new Random();
        public int floor;

        public Stage()
        {
            player = GameManager.Instance.player;
            monsters = GameManager.Instance.monsterList.monsterList;

            floor = 1;
        }

        public List<Monster> SummonMonster()//몬스터 소환
        {
            //1.몬스터 클래스에서 몬스터 정보받고 그정보로 몬스터 랜덤성으로 1~4마리 소환
            List<Monster> list = new List<Monster>();//랜덤 설정된 몬스터를 받는곳

            int monsterObj = Random.Next(1, 5); //몬스터 마리수 결정

            for (int i = 0; i < monsterObj; i++)
            {
                int randomMon = Random.Next(monsters.Count);//리스트 안 객체를 랜덤하게 정하는것
                list.Add(monsters[randomMon]);
            }
            return list; //랜덤설정된 몬스터를 list변수에 보낸다
        }

        public void BattleField()
        {        
            
            //소환됀 객체들을 필드위에 뛰운다 그리고 필드 몬스터 리스트화?
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            List<Monster> list = SummonMonster();// 이리스트를 다른곳에서 써야함

            while (true)
            {                
                foreach (Monster monster in list)//소환된 몬스터 목록 보여주는곳
                {
                    if (monster.dead)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;//글자 어두운 회색으로 변경
                        Console.WriteLine($"Lv.{monster.level} {monster.name} Dead ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"Lv.{monster.level} {monster.name} HP {monster.hp} ");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("[내정보]");
                player.StatusDisplay();
                Console.WriteLine();

                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬 공격");
                Console.Write("원하시는 행동을 입력해주세요 : ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            playerAttackField(list);
                            break;
                            case 2:
                            playerSkillAttackField(list);
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            continue;

                    }
                }
            }
        }
        //2.공격하기 선택창을 추가하여 공격시 전투 계산 메소드 작동
        public void playerAttackField(List<Monster> attackMonster)
        {
            Console.Clear();

            while (true)
            {
                Console.Clear();
                int choiceMonster = 1;
                List<Monster> summon = attackMonster;
                foreach (Monster monster in summon) //소환된 몬스터 개체수 만큼 선택지를 늘리기
                {
                    if (monster.dead == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;//글자 어두운 회색으로 변경
                        Console.WriteLine($"{choiceMonster}. Lv.{monster.level} {monster.name} Dead ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{choiceMonster}. Lv.{monster.level} {monster.name} HP {monster.hp} ");
                    }
                    choiceMonster++;
                }
                Console.WriteLine("[내정보]");
                player.StatusDisplay();
                Console.WriteLine();
                Console.WriteLine($"{choiceMonster}. 취소");
                Console.WriteLine();
                Console.Write("공격할 몬스터를 선택해주세요. : ");

                bool allMonstersDead = attackMonster.All(monster => monster.dead);//모든 몬스터가 죽으면 true 아니면 false 을 반환
                if (allMonstersDead || player.hp == 0) //승리 조건 검사
                {
                    Console.Clear();
                    BattleResult(allMonstersDead, summon.Count);
                    break;
                }

                string playerInput = Console.ReadLine();
                if (int.TryParse(playerInput, out int monIndex) && monIndex > 0 && monIndex <= attackMonster.Count) //입력한숫자가 0이상 선택지
                {
                    Monster selectedMonster = attackMonster[monIndex - 1];// 유저가 선택한 개채 변수에 저장

                    if (selectedMonster.dead)//개체가 죽었을시
                    {
                        Console.WriteLine("이미 죽은 몬스터입니다.");
                    }
                    else
                    {
                        Console.Clear();
                        player.Attack(selectedMonster);//플레이어 공격 메서드에게 선택한 개체정보 보내고 연산뒤 결과값 받기 (플레이어의 공격턴)

                        //다음 선택지 만들기
                        while (true)
                        {
                            Console.WriteLine("0. 다음");
                            int next = int.Parse(Console.ReadLine());
                            if (next == 0)
                            {
                                selectedMonster.Attack(player);//몬스터의 공격 메서드 (몬스터의 공격턴)
                                Console.WriteLine("0. 다음");
                                int next2 = int.Parse(Console.ReadLine());
                                if (next2 == 0)
                                {
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("잘못된 입력입니다.");
                                }

                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }

                    }
                }
                else if (monIndex == choiceMonster)
                {
                    break;//취소
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
                }

            }
        }
        //3.전투가 끝날시 남아있는 현재체력과 현재 스탯표시(결과창)
        public void BattleResult(bool Result, int summonCount)
        {
            bool allMonstersDead = Result;

            if (allMonstersDead)//모든 몬스터가 죽을시
            {
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine();
                Console.WriteLine("Victory");
                Console.WriteLine();

                Console.WriteLine($"던전에서 몬스터 {summonCount}마리를 잡았습니다.");
                Console.WriteLine();
                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"HP {player.maxHp} -> {player.hp}");
                Console.WriteLine();
                GetReward(); // 보상 획득
                Console.WriteLine();


                Console.WriteLine("0. 다음");
                int next = int.Parse(Console.ReadLine());
                if (next == 0)
                {
                    Console.WriteLine("끝");
                }

                // 던전 층 상승
                floor++;
            }

            if (player.hp == 0) //플레이어의 현재체력이 0이 될시
            {
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine();
                Console.WriteLine("You Lose");
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"HP {player.maxHp} -> {player.hp}");
                Console.WriteLine();

                Console.WriteLine("0. 다음");
                int next = int.Parse(Console.ReadLine());
                if (next == 0)
                {
                    Console.WriteLine("끝");
                }
            }
        }

        private void GetReward()
        {
            Random rand = new Random();
            
            int gold = rand.Next(500, 1000);
            // 아이템 리스트에서 랜덤으로 아이템 가져오기
            Item item = GameManager.Instance.itemList.all[rand.Next(0, GameManager.Instance.itemList.all.Count)];

            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{gold} Gold");
            Console.WriteLine($"{item.Name} - 1");
            Console.WriteLine();

            player.gold += gold;
            //player.inven.add(item);
        }

        public void playerSkillAttackField(List<Monster> attackMonster)
        //직업 인식후 직업에 맞는 스킬 리스트 가져오기
        {
            Console.Clear();
            int choiceMonster = 1;
            List<Monster> summon = attackMonster;
            foreach (Monster monster in summon) //소환된 몬스터 개체수 만큼 선택지를 늘리기
            {
                if (monster.dead == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;//글자 어두운 회색으로 변경
                    Console.WriteLine($"{choiceMonster}. Lv.{monster.level} {monster.name} Dead ");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{choiceMonster}. Lv.{monster.level} {monster.name} HP {monster.hp} ");
                }
                choiceMonster++;
            }
            Console.WriteLine("[내정보]");
            player.StatusDisplay();
            Console.WriteLine();
            Console.WriteLine($"{choiceMonster}. 취소");
            Console.WriteLine();
            Console.Write("공격할 몬스터를 선택해주세요. : ");


            bool allMonstersDead = attackMonster.All(monster => monster.dead);//모든 몬스터가 죽으면 true 아니면 false 을 반환
            if (allMonstersDead || player.hp == 0) //승리 조건 검사
            {
                Console.Clear();
                BattleResult(allMonstersDead, summon.Count);
            }

            string playerInput = Console.ReadLine();
            if (int.TryParse(playerInput, out int monIndex) && monIndex > 0 && monIndex <= attackMonster.Count) //입력한숫자가 0이상 선택지
            {
                Monster selectedMonster = attackMonster[monIndex - 1];// 유저가 선택한 개채 변수에 저장

                if (selectedMonster.dead)//개체가 죽었을시
                {
                    Console.WriteLine("이미 죽은 몬스터입니다.");
                }
                else
                {
                    Console.Clear();

                    //skill.SkillAttack(player, selectedMonster, SelectedjobSkill(player));

                    skill = CommonUtil.SelectedjobSkill(player); //지정한 스킬의 데이터를 스킬 클래스를 변수로 지정한곳에 보낸다
                    Console.Clear();
                    skill.SkillAttack(player, selectedMonster);

                    //CommonUtil.SelectedjobSkill(player).SkillAttack(player, selectedMonster);

                    //다음 선택지 만들기
                    while (true)
                    {
                        Console.WriteLine("0. 다음");
                        int next = int.Parse(Console.ReadLine());
                        if (next == 0)
                        {
                            Console.Clear();
                            selectedMonster.Attack(player);//몬스터의 공격 메서드 (몬스터의 공격턴)
                            Console.WriteLine("0. 다음");
                            int next2 = int.Parse(Console.ReadLine());
                            if (next2 == 0)
                            {
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }

                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }

                }
            }

        }

        
    }
}

