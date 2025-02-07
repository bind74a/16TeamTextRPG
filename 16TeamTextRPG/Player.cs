using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16TeamTextRPG
{
    public class Player
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int Atk { get; set; }

        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }

        //플레이어 생성자
        public Player(int level, string name, string job, int atk, int def, int hp, int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }

        //상태창에 나올 스텟시트
        public void StatusDisplay()
        {
            Console.WriteLine($"Lv {Level.ToString("00")}");
            Console.WriteLine($"{Name} ({Job})");
            Console.WriteLine($"공격력 : {Atk}");
            Console.WriteLine($"방어력 : {Def}");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G");
        }
    }
}
