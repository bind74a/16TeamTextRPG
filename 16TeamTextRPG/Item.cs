using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _16TeamTextRPG
{
    public class Item
    {
        public string Name { get; } // 아이템 이름
        public string Option { get; }   // 아이템 옵션
        public string Info { get; } // 아이템 설명
        public string Type { get; protected set; }  // 아이템 타입( 무기, 방어구, 악세사리, 소모품)

        public int Price { get; }   // 아이템 가격
        public int Atk { get; }   // 아이템 공격력
        public int Def { get; }   // 아이템 방어력
        public int Hp { get; }   // 아이템 체력

        public bool CanBuy; // true = 구매 가능한 상태 (기본값 true)
        public bool IsEquip;    // true = 착용한 상태 (기본값 false)

        public Item(ref Stat stat)
        {
            Name = stat.Name;
            Option = stat.Option;
            Info = stat.Info;
            Price = stat.Price;
            Atk = stat.Atk;
            Def = stat.Def;
            Hp = stat.Hp;

            CanBuy = true;
            IsEquip = false;
            stat.Init();
        }

        public void EquipItem(Player player)
        {
            IsEquip = IsEquip == false ? true : false;
            int sign = IsEquip == true ? 1 : -1;

            player.atk += Atk;
            player.def += Def;
            player.hp += Hp;
        }
    }

    public struct Stat
    {
        public string Name = ""; // 아이템 이름
        public string Option = "";   // 아이템 옵션
        public string Info = ""; // 아이템 설명
        public string Type = "";  // 아이템 타입( 무기, 방어구, 악세사리, 소모품)

        public int Price = 0;   // 아이템 가격
        public int Atk = 0;   // 아이템 공격력
        public int Def = 0; // 아이템 방어력
        public int Hp = 0;   // 아이템 체력

        public bool CanBuy = true; // true = 구매 가능한 상태 (기본값 true)
        public bool IsEquip = false;    // true = 착용한 상태 (기본값 false)

        public Stat()
        {

        }

        public void Init()  // 아이템 초기화
        {
            Name = ""; 
            Option = "";   
            Info = ""; 
            Type = "";  

            Price = 0;  
            Atk = 0;  
            Def = 0; 
            Hp = 0;   

            CanBuy = true; 
            IsEquip = false; 
        }
    }

    public class Weapon : Item  // 무기
    {
        public Weapon(ref Stat stat) : base(ref stat)
        {
            Type = "weapon";
        }
    }

    public class Armor : Item  // 방어구
    {
        public Armor(ref Stat stat) : base(ref stat)
        {
            Type = "armor";
        }
    }

    public class Accessory : Item  // 악세서리
    {
        public Accessory(ref Stat stat) : base(ref stat)
        {
            Type = "accessory";
        }
    }

    public class Consumable : Item  // 소모품
    {
        public Consumable(ref Stat stat) : base(ref stat)
        {
            Type = "consumable";
        }
    }

    public class ItemList   // 아이템 리스트
    {
        Stat stat = new Stat();
        public List<Item> all = new List<Item>();   // 전체 아이템 리스트

        public ItemList()
        {
            stat.Name = "낡은 검"; stat.Option = "공격력 +2"; stat.Info = "쉽게 볼 수 있는 낡은 검 입니다."; stat.Price = 600; stat.Atk = 2;
            all.Add(new Weapon(ref stat));
            stat.Name = "청동 도끼"; stat.Option = "공격력 +5"; stat.Info = "어디선가 사용됐던거 같은 도끼입니다."; stat.Price = 1500; stat.Atk = 5;
            all.Add(new Weapon(ref stat));
            stat.Name = "스파르타의 창"; stat.Option = "공격력 +7"; stat.Info = "스파르타의 전사들이 사용했다는 전설의 창입니다."; stat.Price = 2500; stat.Atk = 7;
            all.Add(new Weapon(ref stat));
            stat.Name = "수련자 갑옷"; stat.Option = "방어력 +5"; stat.Info = "수련에 도움을 주는 갑옷입니다."; stat.Price = 1000; stat.Def = 5;
            all.Add(new Armor(ref stat));
            stat.Name = "무쇠갑옷"; stat.Option = "방어력 +9"; stat.Info = "무쇠로 만들어져 튼튼한 갑옷입니다."; stat.Price = 2000; stat.Def = 9;
            all.Add(new Armor(ref stat));
            stat.Name = "스파르타의 갑옷"; stat.Option = "방어력 +15"; stat.Info = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."; stat.Price = 3500; stat.Def = 15;
            all.Add(new Armor(ref stat));
        }

    }
}
