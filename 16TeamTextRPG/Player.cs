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
        public int gold { get; set; }

        public GameManager.Job playJob { get; set; }

        //플레이어 생성자
        public Player(int level, string name, string job, int atk, int def, int hp, int gold)
        {
            this.level = level;
            this.name = name;
            this.job = job;
            this.atk = atk;
            this.def = def;
            this.hp = hp;
            this.gold = gold;
        }

        //상태창에 나올 스텟시트
        public void StatusDisplay()
        {
            Console.WriteLine($"Lv {level.ToString("00")}");
            Console.WriteLine($"{name} ({job})");
            Console.WriteLine($"공격력 : {atk}");
            Console.WriteLine($"방어력 : {def}");
            Console.WriteLine($"체력 : {hp}");
            Console.WriteLine($"Gold : {gold} G");
        }

        public void Attack(Monster monster)
        {
            Random rand = new Random();

            // 공격력의 10% 오차 (오차가 소수점이라면 올림처리);
            int min = atk - (int)Math.Ceiling(atk * 0.1f);
            int max = atk + (int)Math.Ceiling(atk * 0.1f);

            // 최종 공격력
            int final = rand.Next(min, max + 1);

            monster.hp -= final;
            if (monster.hp <= 0)
            {
                //monster.dead = true;
                monster.hp = 0;
            }

            // Consol UI
            Console.WriteLine($"Lv.{level} {name}의 공격!"); // Lv.2 미니언의 공격!
            Console.WriteLine($"{monster.name}을(를) 맞췄습니다. [데미지: {final}]\n"); // Chad 을(를) 맞췄습니다. [데미지: 6]

            Console.WriteLine($"Lv.{monster.level} {monster.name}"); // Lv.1 Chad
            Console.WriteLine($"HP {monster.hp} -> {monster.hp}"); // HP 100 -> 94
        }
    }
}
