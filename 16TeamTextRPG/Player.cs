using _16TeamTexTRPG;
using System.ComponentModel.Design;
using System.Numerics;

namespace _16TeamTextRPG
{
    public class Player
    {
        public int level { get; set; }
        public string name { get; set; }
        public string job { get; set; }
        public int atk { get; set; }
        public int Atk
        {
            get
            {
                Item item = GameManager.Instance.inventory.equipWeapon;
                return atk + (item == null ? 0 : item.Atk);
            }
        }

        public int def { get; set; }
        public int Def
        {
            get
            {
                Item item = GameManager.Instance.inventory.equipArmor;
                return def + (item == null ? 0 : item.Def);
            }
        }
        public int maxHp { get; set; }
        public int hp { get; set; }
        public int maxMp { get; set; }
        public int mp { get; set; }
        public int gold { get; set; }
        public int exp { get; set; }
        public int levelUpforExp { get; set; }


        public GameManager.Job playJob { get; set; }

        Json json;

        //플레이어 생성자
        public Player(int level, string name, string job, int atk, int def, int maxHp, int gold , int exp, int levelUpforExp)
        {
            json = new Json("Player.json");

            this.level = level;
            this.name = name;
            this.job = job;
            this.atk = atk;
            this.def = def;
            this.maxHp = maxHp;
            this.hp = maxHp;
            this.maxMp = 100;
            this.mp = maxMp;
            this.gold = gold;
            this.exp = exp;
            this.levelUpforExp = levelUpforExp;
        }

        public void Free()
        {
            json.Save(this);
        }

        public bool Load()
        {
            Player LoadPlayer = json.Load<Player>();

            if (LoadPlayer == null)
                return false;

            this.level = LoadPlayer.level;
            this.name = LoadPlayer.name;
            this.job = LoadPlayer.job;
            this.atk = LoadPlayer.atk;
            this.def = LoadPlayer.def;
            this.maxHp = LoadPlayer.maxHp;
            this.hp = LoadPlayer.maxHp;
            this.maxMp = 100;
            this.mp = LoadPlayer.maxMp;
            this.gold = LoadPlayer.gold;
            this.exp = LoadPlayer.exp;
            this.levelUpforExp = LoadPlayer.levelUpforExp;

            return true;
        }

        //상태창에 나올 스텟시트
        public void StatusDisplay()
        {
            Console.WriteLine($"Lv {level.ToString("00")}");
            Console.WriteLine($"{name} ({job})");
            Console.WriteLine($"공격력 : {atk}");
            Console.WriteLine($"방어력 : {def}");
            Console.WriteLine($"체력 : {hp} / {maxHp}");
            Console.WriteLine($"마력 : {mp} / {maxMp}");
            Console.WriteLine($"Gold : {gold} G");
            Console.WriteLine($"현재 경험치 : {exp}");
            Console.WriteLine($"다음 레벨까지 필요한 경험치 : {levelUpforExp - exp} ");
        }

        public void Attack(Monster monster)
        {
            Random rand = new Random();

            // 공격력의 10% 오차 (오차가 소수점이라면 올림처리);
            int min = Atk - (int)Math.Ceiling(atk * 0.1f);
            int max = Atk + (int)Math.Ceiling(atk * 0.1f);
            int BaseDamage = rand.Next(min, max + 1);
            //int BaseDamage = atk; // 장비데미지 추가 필요

            // 최종 공격력
            int finalDamage = 0;
            bool isCriticalDamage = rand.Next(100) <= 15; // 15%의 확률로 크리티컬 데미지 발생
            bool isMissDamage = rand.Next(100) <= 20; // 20%의 확률로 회피 발생
            //int final = rand.Next(min, max + 1);
            int lastHp = monster.hp;

            //if (isMissDamage) // 몬스터 회피 너무 불쾌함
            //{
            //    finalDamage = 0;
            //    //Console.WriteLine("{monster.name}(이)가 회피에 성공하였습니다다. [데미지 : {final}]");
            //}

            if (isCriticalDamage)
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
                monster.dead = true;
                monster.hp = 0;
                GameManager.Instance.guild.UpdateProgress(0); // 몬스터 처치시 퀘스트 목표 업데이트
            }

            // Consol UI
            if (isCriticalDamage)
                CommonUtil.WriteLine($"Lv.{level} {name}의 크리티컬공격!!", ConsoleColor.DarkRed);
            else
                Console.WriteLine($"Lv.{level} {name}의 공격!"); // Lv.2 미니언의 공격!
            Console.WriteLine($"{monster.name}을(를) 맞췄습니다. [데미지: {finalDamage}]\n");

            Console.WriteLine($"Lv.{monster.level} {monster.name}"); // Lv.1 Chad
            Console.WriteLine($"HP {lastHp} -> {monster.hp}"); // HP 100 -> 94

            // 몬스터 처치 시
            if (monster.dead)
                GainExp(monster.exp);

            Console.WriteLine();
        }

        //경험치 얻었을 경우의 함수
        public void GainExp(int Exp)
        {
            exp += Exp;
            Console.WriteLine($"경험치 {Exp}를 얻었습니다.");

            totalExp();
            //if(level == 1)
            //{
            //    levelUpforExp = 10;
            //    totalExp();
            //}
            //else if(level == 2)
            //{
            //    levelUpforExp = 35;
            //    totalExp();
            //}
            //else if (level == 3)
            //{
            //    levelUpforExp = 65;
            //    totalExp();
            //}
            //else if (level == 4)
            //{
            //    levelUpforExp = 100;
            //    totalExp();
            //}
        }

        public void totalExp()
        {
            if (levelUpforExp <= exp) 
            {
                CommonUtil.WriteLine("LevelUp!!", ConsoleColor.DarkRed);
                Console.Write($"Lv: {level} -> "); CommonUtil.WriteLine($"{++level}", ConsoleColor.DarkRed);
                Console.Write($"MaxHp: {maxHp} -> "); CommonUtil.WriteLine($"{maxHp += 10}", ConsoleColor.DarkRed);
                Console.Write($"Atk: {atk} -> "); CommonUtil.WriteLine($"{++atk}", ConsoleColor.DarkRed);
                
                levelUpforExp *= level;
                exp = 0;
                //atk++;
                def++;
            }
        }
    }
}
