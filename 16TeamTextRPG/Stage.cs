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

        int playerLastHP;

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
                list.Add(new Monster(monsters[randomMon]));
            }
            return list; //랜덤설정된 몬스터를 list변수에 보낸다
        }

        public void BattleField()
        {
            // 던전에 들어오기전 플레이어의 마지막 체력
            playerLastHP = player.hp;
            //소환됀 객체들을 필드위에 뛰운다 그리고 필드 몬스터 리스트화?
            List<Monster> list = SummonMonster();// 이리스트를 다른곳에서 써야함

            while (true)
            {
                bool allMonstersDead = list.All(monster => monster.dead);//모든 몬스터가 죽으면 true 아니면 false 을 반환
                if (allMonstersDead || player.hp <= 0) //승리 조건 검사
                {
                    Console.Clear();
                    BattleResult(allMonstersDead, list.Count);
                    break;
                }

                Console.Clear();
                CommonUtil.WriteLine("Battle!!", ConsoleColor.DarkYellow);
                Console.WriteLine();
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
                Console.WriteLine("0. 도망가기\n");

                int choice = CommonUtil.CheckInput(0, 2);
                if (choice == 0)
                    break;

                switch (choice)
                {
                    case 1:
                        playerAttackField(list); // 일반 공격
                        break;
                    case 2:
                        playerSkillAttackField(list); // 스킬 공격
                        break;
                }
            }
        }
        //2.공격하기 선택창을 추가하여 공격시 전투 계산 메소드 작동
        public void playerAttackField(List<Monster> attackMonster)
        {
            Console.Clear();
            CommonUtil.WriteLine("Battle!!", ConsoleColor.DarkYellow);
            Console.WriteLine();

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

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            player.StatusDisplay();
            Console.WriteLine();
            //Console.WriteLine($"{choiceMonster}. 취소");
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            //Console.Write("공격할 몬스터를 선택해주세요. : ");

            int monIndex = CommonUtil.CheckInput(0, attackMonster.Count);
            if (monIndex == 0) // 공격 취소
                return;

            Monster selectedMonster = attackMonster[monIndex - 1];// 유저가 선택한 개채 변수에 저장
            if (selectedMonster.dead)//개체가 죽었을시
            {
                Console.WriteLine("이미 죽은 몬스터입니다.");
                Thread.Sleep(500);
            }
            else
            {
                // Player Turn
                Console.Clear();
                CommonUtil.WriteLine("Battle!! - Player Phase\n", ConsoleColor.DarkYellow);

                player.Attack(selectedMonster);//플레이어 공격 메서드에게 선택한 개체정보 보내고 연산뒤 결과값 받기 (플레이어의 공격턴)

                //다음 선택지 만들기
                Console.WriteLine("0. 다음\n");
                CommonUtil.CheckInput(0, 0);

                if (selectedMonster.dead) // 방금 플레이어의 공격으로 몬스터가 죽었다면 종료
                    return;

                // Enemy Turn
                Console.Clear();
                CommonUtil.WriteLine("Battle!! - Enemy Phase\n", ConsoleColor.DarkYellow);

                selectedMonster.Attack(player); // 몬스터의 공격 메서드 (몬스터의 공격턴)
                Console.WriteLine("0. 다음\n");
                CommonUtil.CheckInput(0, 0);
            }
        }
        //3.전투가 끝날시 남아있는 현재체력과 현재 스탯표시(결과창)
        public void BattleResult(bool Result, int summonCount)
        {
            bool allMonstersDead = Result;

            CommonUtil.WriteLine("Battle!! - Result", ConsoleColor.DarkYellow);
            Console.WriteLine();

            if (allMonstersDead)//모든 몬스터가 죽을시
            {
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

                Console.WriteLine("0. 돌아가기\n");
                CommonUtil.CheckInput(0, 0);

                // 던전 층 상승
                floor++;
            }

            if (player.hp <= 0) //플레이어의 현재체력이 0이 될시
            {
                Console.WriteLine("You Lose");
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"HP {playerLastHP} -> {player.hp}");
                Console.WriteLine();

                Console.WriteLine("0. 돌아가기\n");
                CommonUtil.CheckInput(0, 0);
            }
        }

        private void GetReward()
        {
            Random rand = new Random();
            
            int gold = rand.Next(5, 10) * 100;
            // 아이템 리스트에서 랜덤으로 아이템 가져오기
            Item item = new Item(GameManager.Instance.itemList.all[rand.Next(0, GameManager.Instance.itemList.all.Count)]);

            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{gold} Gold");
            Console.WriteLine($"{item.Name}");

            player.gold += gold;
            GameManager.Instance.inventory.list.Add(item);
        }

        public void playerSkillAttackField(List<Monster> attackMonster)
        //직업 인식후 직업에 맞는 스킬 리스트 가져오기
        {
            Console.Clear();
            CommonUtil.WriteLine("Battle!!", ConsoleColor.DarkYellow);
            Console.WriteLine();

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

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            player.StatusDisplay();
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();

            int monIndex = CommonUtil.CheckInput(0, attackMonster.Count);

            if (monIndex == 0) // 스킬 공격 취소
                return;

            Monster selectedMonster = attackMonster[monIndex - 1];// 유저가 선택한 개채 변수에 저장

            if (selectedMonster.dead)//개체가 죽었을시
            {
                Console.WriteLine("이미 죽은 몬스터입니다.");
                Thread.Sleep(500);
            }
            else
            {
                ///// Player Turn
                Console.Clear();
                //skill.SkillAttack(player, selectedMonster, SelectedjobSkill(player));
                skill = CommonUtil.SelectedjobSkill(player); //지정한 스킬의 데이터를 스킬 클래스 변수로 지정한곳에 보낸다
                Console.Clear();
                CommonUtil.WriteLine("Battle!! - Player Phase\n", ConsoleColor.DarkYellow);
                skill.SkillAttack(selectedMonster);
                //CommonUtil.SelectedjobSkill(player).SkillAttack(selectedMonster); // .을기준으로 왼쪽의 데이터가 오른쪽으로 옮겨지면서 기동

                //다음 선택지 만들기
                Console.WriteLine();
                Console.WriteLine("0. 다음\n");
                CommonUtil.CheckInput(0, 0);
                
                if (selectedMonster.dead) // 방금 플레이어의 공격으로 몬스터가 죽었다면 종료
                    return;

                ///// Enemy Turn
                Console.Clear();
                CommonUtil.WriteLine("Battle!! - Enemy Phase\n", ConsoleColor.DarkYellow);
                selectedMonster.Attack(player);//몬스터의 공격 메서드 (몬스터의 공격턴)
                
                Console.WriteLine("0. 다음\n");
                CommonUtil.CheckInput(0, 0);
                Console.Clear();
            }
        }
    }
}

