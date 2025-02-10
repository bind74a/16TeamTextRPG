using _16TeamTextRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _16TeamTextRPG
{
    public class Monster
    {
<<<<<<< Updated upstream
        public string name;
        public bool dead;
        public int level;
        public int maxHp;
        public int hp;
        public int gold;
        public int exp;
        public int atk;
        public int def;
=======
        string name;
        bool dead;
        int level;
        int maxHealth;
        int health;
        int gold;
        int exp;
        float attack;
        float defense;
>>>>>>> Stashed changes

        public Monster(string _name, int _level, int _hp, int _atk)
        {
<<<<<<< Updated upstream
            name = _name;
            dead = false;
            level = _level;
            maxHp = _hp;
            hp = maxHp;
            gold = 0;
            exp = 0;
            atk = _atk;
            def = 0;
=======
            name        = "";
            dead        = false;
            level       = 0;
            maxHealth   = 0;
            health      = 0;
            gold        = 0;
            exp         = 0;
            attack      = 0f;
            defense     = 0f;
>>>>>>> Stashed changes
        }

        public void Attack(Player player)
        {
            Random rand = new Random();
<<<<<<< Updated upstream

            // 공격력의 10% 오차 (오차가 소수점이라면 올림처리);
            int min = atk - (int)Math.Ceiling(atk * 0.1f);
            int max = atk + (int)Math.Ceiling(atk * 0.1f);

            // 최종 공격력
            int final = rand.Next(min, max + 1);

            player.hp -= final;
            if (player.hp <= 0)
            {
                //player.dead = true;
                player.hp = 0;
            }

            // Consol UI
            Console.WriteLine($"Lv.{level} {name}의 공격!"); // Lv.2 미니언의 공격!
            Console.WriteLine($"{player.name}을(를) 맞췄습니다. [데미지: {final}]\n"); // Chad 을(를) 맞췄습니다. [데미지: 6]

            Console.WriteLine($"Lv.{player.level} {player.name}"); // Lv.1 Chad
            Console.WriteLine($"HP {player.hp} -> {player.hp}"); // HP 100 -> 94
        }
    }

    // Monster DataBase
    public class MonsterList
    {
        public List<Monster> monsterList;
        
        public MonsterList()
        {
            monsterList = new List<Monster>();
            
            monsterList.Add(new Monster("미니언", 2, 15, 5));
            monsterList.Add(new Monster("공허충", 3, 10, 9));
            monsterList.Add(new Monster("대포미니언", 5, 25, 8));
            monsterList.Add(new Monster("마법사미니언", 4, 8, 11));
=======
            rand.Next();
            // 공격력의 10% 오차
            float ratio = attack * 0.1f;
>>>>>>> Stashed changes
        }
    }
}

