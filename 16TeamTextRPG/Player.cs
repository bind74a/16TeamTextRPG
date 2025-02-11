using System.Numerics;

namespace _16TeamTextRPG
{
    public class Player
    {
        public int level { get; set; }
        public string name { get; set; }
        public string job { get; set; }
        public int atk { get; set; }

        public int def { get; set; }
        public int maxHp { get; set; }
        public int hp { get; set; }
        public int maxMp { get; set; }
        public int mp { get; set; }
        public int gold { get; set; }
        public int exp { get; set; }
        public int levelUpforExp { get; set; }


        public GameManager.Job playJob { get; set; }

        //플레이어 생성자
        public Player(int level, string name, string job, int atk, int def, int maxHp, int gold , int exp, int levelUpforExp)

        {
            this.level = level;
            this.name = name;
            this.job = job;
            this.atk = atk;
            this.def = def;
            this.hp = maxHp;
            this.maxHp = maxHp;
            this.maxMp = 100;
            this.mp = maxMp;
            this.gold = gold;
            this.exp = exp;
            this.levelUpforExp = levelUpforExp;

        }

        //상태창에 나올 스텟시트
        public void StatusDisplay()
        {
            Console.WriteLine($"Lv {level.ToString("00")}");
            Console.WriteLine($"{name} ({job})");
            Console.WriteLine($"공격력 : {atk}");
            Console.WriteLine($"방어력 : {def}");
            Console.WriteLine($"체력 : {hp} / {maxHp}");
            Console.WriteLine($"Gold : {gold} G");
            Console.WriteLine($"현재 경험치 : {exp}");
            Console.WriteLine($"다음 레벨까지 필요한 경험치 : {levelUpforExp - exp} ");

        }

        public void Attack(Monster monster)
        {
            Random rand = new Random();

            // 공격력의 10% 오차 (오차가 소수점이라면 올림처리);
            int BaseDamage = atk; // 
            /*int min = atk - (int)Math.Ceiling(atk * 0.1f);
            int max = atk + (int)Math.Ceiling(atk * 0.1f);*/

            // 최종 공격력

            int finalDamage = 0;
            //int final = rand.Next(min, max + 1);
            bool isCriticalDamage = rand.Next(100) <= 15; // 15%의 확률로 크리티컬 데미지 발생
            bool isMissDamage = rand.Next(100) <= 20; // 20%의 확률로 회피 발생
            //int final = rand.Next(min, max + 1);
            int lastHp = monster.hp;

            if (isMissDamage)
            {
                finalDamage = 0;
                Console.WriteLine("{monster.name}(이)가 회피에 성공하였습니다다. [데미지 : {final}]");
            }

            else if (isCriticalDamage)
            {
                finalDamage = (int)(BaseDamage + (BaseDamage * 1.6)); // 크리티컬이 발생하였을 때 적용되는 공식
            }
            else if (!isCriticalDamage)
            {
                finalDamage = BaseDamage; // 크리티컬이랑 회피가 발생하지 않을 경우 호출되는 함수수
            }

            monster.hp -= finalDamage;
            if (monster.hp <= 0)
            {
                //player.dead = true;
                monster.hp = 0;
                GainExp(monster.exp);
            }

            // Consol UI
            Console.WriteLine($"Lv.{level} {name}의 공격!"); // Lv.2 미니언의 공격!
            Console.WriteLine($"{monster.name}을(를) 맞췄습니다. [데미지: {finalDamage}]\n"); // Chad 을(를) 맞췄습니다. [데미지: 6]

            Console.WriteLine($"Lv.{monster.level} {monster.name}"); // Lv.1 Chad
            Console.WriteLine($"HP {lastHp} -> {monster.hp}"); // HP 100 -> 94
        }

        //경험치 얻었을 경우의 함수
        public void GainExp(int Exp)
        {
            exp += Exp;
            Console.WriteLine($"경험치 {Exp}를 얻었습니다.");

            if(level == 1)
            {
                levelUpforExp = 10;
                totalExp();
            }
            else if(level == 2)
            {
                levelUpforExp = 35;
                totalExp();
            }
            else if (level == 3)
            {
                levelUpforExp = 65;
                totalExp();
            }
            else if (level == 4)
            {
                levelUpforExp = 100;
                totalExp();
            }

            
        }

        public void totalExp()
        {
            if (levelUpforExp <= exp) 
            {
                level++;
                exp = 0;
                atk++;
                def++;
            }
        }
    }
}
