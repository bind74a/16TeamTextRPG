using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16TeamTextRPG
{
    internal class Item
    {
        public string Name { get; } // 아이템 이름
        public string Option { get; }   // 아이템 옵션
        public string Info { get; } // 아이템 설명
        public string Type { get; protected set; }  // 아이템 타입( 무기, 방어구, 악세사리, 소모품)

        public int Price { get; }   // 아이템 가격

        public bool CanBuy; // true = 구매 가능한 상태 (기본값 true)
        public bool IsEquip;    // true = 착용한 상태 (기본값 false)

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

    public class Weapon : Item  // 무기
    {
        public Weapon(string name, string option, string info, int price) : base(name, option, info, price)
        {
            Type = "weapon";
        }
    }

    public class ItemList   // 아이템 리스트
    {
        public List<Item> all = new List<Item>()    // 전체 아이템 리스트
        {
            new Weapon("낡은 검", "공격력 + 5", "금방이라도 부러질 것 같은 낡은 검 입니다.", 500)
        };
    }
}
