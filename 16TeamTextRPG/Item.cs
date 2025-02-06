using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16TeamTextRPG
{
    internal class Item
    {
        public string Name { get; }
        public string Option { get; }
        public string Info { get; }
        public string Type { get; protected set; }

        public int Price { get; }

        public bool CanBuy;
        public bool IsEquip;

        public Item(string name, string option, string info, int price)
        {
            Name = name;
            Option = option;
            Info = info;
            Price = price;

            CanBuy = true;
            IsEquip = false;
        }
    }

    public class DataBase
    {
        public List<Item> allItems = new List<Item>()
    {
        new Weapon("낡은 검", "공격력 + 5", "금방이라도 부러질 것 같은 낡은 검 입니다.", 500)
    };
    }
    public class Weapon : Item
    {
        public Weapon(string name, string option, string info, int price) : base(name, option, info, price)
        {
            Type = "weapon";
        }
    }
}
