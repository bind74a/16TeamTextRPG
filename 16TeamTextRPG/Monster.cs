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
    }
}
