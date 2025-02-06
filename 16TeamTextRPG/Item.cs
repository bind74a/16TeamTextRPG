using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16TeamTextRPG
{
    internal class Item
    {
<<<<<<< Updated upstream
=======
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

    public interface EquipmentItem // 장비 아이템 ( 무기, 방어구, 악세서리에 상속)
    {
        public int Power { get; }   // 아이템 공격력
        public int Defense { get; }   // 아이템 방어력
        public int Health { get; }   // 아이템 체력

        public void EquipItem(Character player)
        {
            IsEquip = IsEquip == false ? true : false;
            int sign = IsEquip == true ? 1 : -1;

            player.power += Power;
            player.defense += Defense;
            player.health += Health;
        }
    }

    public class Weapon : Item, EquipmentItem  // 무기
    {
        public Weapon(string name, string option, string info, int price) : base(name, option, info, price)
        {
            Type = "weapon";
        }
    }
    public class Armor : Item, EquipmentItem  // 방어구
    {
        public Armor(string name, string option, string info, int price) : base(name, option, info, price)
        {
            Type = "armor";
        }
    }

    public class Accessory : Item, EquipmentItem  // 악세서리
    {
        public Weapon(string name, string option, string info, int price) : base(name, option, info, price)
        {
            Type = "accessory";
        }
    }

    public class Consumable : Item  // 소모품
    {
        public Armor(string name, string option, string info, int price) : base(name, option, info, price)
        {
            Type = "consumable";
        }
    }

    public class ItemList   // 아이템 리스트
    {
        public List<Item> all = new List<Item>()    // 전체 아이템 리스트
        {
            new Weapon("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", 600)
            new Weapon("청동 도끼", "공격력 +5", "어디선가 사용됐던거 같은 도끼입니다.", 1500)
            new Weapon("스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500)
            new Armor("수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", 1000)
            new Armor("무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000)
            new Armor("스파르타의 갑옷", "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500)
        };
>>>>>>> Stashed changes
    }
}
