using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16TeamTextRPG
{
    internal class Monster
    {
        string name;
        bool dead;
        int level;
        int maxHealth;
        int health;
        int gold;
        int exp;
        int attack;
        int defense;

        public Monster()
        {
            name = "";
            dead = false;
            level = 0;
            maxHealth = 0;
            health = 0;
            gold = 0;
            exp = 0;
            attack = 0;
            defense = 0;
        }

        public void Attack(Player player)
        {
            Random rand = new Random();

            // 공격력의 10% 오차 (오차가 소수점이라면 올림처리);
            int min = attack - (int)Math.Ceiling(attack * 0.1f);
            int max = attack + (int)Math.Ceiling(attack * 0.1f);

            // 최종 공격력
            int final = rand.Next(min, max + 1);

            player.health -= final;
            if (player.health <= 0)
            {
                player.dead = true;
                player.health = 0;
            }

            // Consol UI
            Console.WriteLine($"Lv.{level} {name}의 공격!"); // Lv.2 미니언의 공격!
            Console.WriteLine($"{player.name}을(를) 맞췄습니다. [데미지: {final}]\n"); // Chad 을(를) 맞췄습니다. [데미지: 6]

            Console.WriteLine($"Lv.{player.level} {player.name}"); // Lv.1 Chad
            Console.WriteLine($"HP {player.health} -> {player.health}"); // HP 100 -> 94
        }
    }
}
