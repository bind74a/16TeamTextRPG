using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using _16TeamTexTRPG;

namespace _16TeamTextRPG
{
    public class Item
    {
        public string Name { get; set; } // 아이템 이름
        public string Option { get; set; }   // 아이템 옵션
        public string Info { get; set; } // 아이템 설명
        public string Type { get; set; }  // 아이템 타입( 무기, 방어구, 악세사리, 소모품)

        public int Price { get; set; }   // 아이템 가격
        public int Atk { get; set; }   // 아이템 공격력
        public int Def { get; set; }   // 아이템 방어력
        public int Hp { get; set; }   // 아이템 체력
        public int Mp { get; set; }   // 아이템 마력
        public int MaxHp { get; set; } // 아이템 최대 체력


        public bool CanBuy; // true = 구매 가능한 상태 (기본값 true)
        public bool IsEquip;    // true = 착용한 상태 (기본값 false)

        public Item() { }

        public Item(ref Stat stat)
        {
            Name = stat.Name;
            Option = stat.Option;
            Info = stat.Info;
            Price = stat.Price;
            Atk = stat.Atk;
            Def = stat.Def;
            Hp = stat.Hp;
            MaxHp = stat.MaxHp;
            Type = stat.Type;

            CanBuy = true;
            IsEquip = false;
            stat.Init();
        }

        public Item(Item item)
        {
            Name = item.Name;
            Option = item.Option;
            Info = item.Info;
            Price = item.Price;
            Atk = item.Atk;
            Def = item.Def;
            Hp = item.Hp;
            MaxHp = item.MaxHp;
            Type = item.Type;

            CanBuy = true;
            IsEquip = false;
        }

        public void EquipOption(Player player)
        {
            int sign = IsEquip == true ? 1 : -1;

            player.atk += Atk * sign;
            player.def += Def * sign;
            player.hp += Hp * sign;
            player.mp += Mp * sign;
            player.maxHp += MaxHp * sign;
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
        public int Mp = 0;   // 아이템 체력
        public int MaxHp = 0;

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
            Mp = 0;

            CanBuy = true; 
            IsEquip = false; 
        }
    }

    public class ItemList   // 아이템 리스트
    {
        Stat stat = new Stat();
        public List<Item> all = new List<Item>();   // 전체 아이템 리스트

        public ItemList()
        {
            stat.Name = "    낡은 검     "; stat.Option = " 공격력  +2 "; stat.Info = " 쉽게 볼 수 있는 낡은 검 입니다.                    "; stat.Price = 600; stat.Atk = 2; stat.Type = "weapon";
            all.Add(new Item(ref stat));
            stat.Name = "   청동 도끼    "; stat.Option = " 공격력  +5 "; stat.Info = " 어디선가 사용됐던거 같은 도끼입니다.               "; stat.Price = 1500; stat.Atk = 5; stat.Type = "weapon";
            all.Add(new Item(ref stat));
            stat.Name = " 스파르타의 창  "; stat.Option = " 공격력  +7 "; stat.Info = " 스파르타의 전사들이 사용했다는 전설의 창입니다.    "; stat.Price = 2500; stat.Atk = 7; stat.Type = "weapon";
            all.Add(new Item(ref stat));
            stat.Name = "  수련자 갑옷   "; stat.Option = " 방어력  +5 "; stat.Info = " 수련에 도움을 주는 갑옷입니다.                     "; stat.Price = 1000; stat.Def = 5; stat.Type = "armor";
            all.Add(new Item(ref stat));
            stat.Name = "   무쇠갑옷     "; stat.Option = " 방어력  +9 "; stat.Info = " 무쇠로 만들어져 튼튼한 갑옷입니다.                 "; stat.Price = 2000; stat.Def = 9; stat.Type = "armor";
            all.Add(new Item(ref stat));
            stat.Name = "스파르타의 갑옷 "; stat.Option = " 방어력 +15 "; stat.Info = " 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.  "; stat.Price = 3500; stat.Def = 15; stat.Type = "armor";
            all.Add(new Item(ref stat));
            //stat.Name = " 소형 HP 포션   "; stat.Option = " 체  력 +20 "; stat.Info = " 체력을 회복 시켜주는 작은 포션입니다."; stat.Price = 500; stat.Hp = 20; stat.Type = "consumable_hp";
            //all.Add(new Item(ref stat));
            //stat.Name = " 소형 MP 포션   "; stat.Option = " 마  력 +20 "; stat.Info = " 마력을 회복 시켜주는 작은 포션입니다."; stat.Price = 500; stat.Mp = 20; stat.Type = "consumable_mp";
            //all.Add(new Item(ref stat));
        }

    }

    public class Inventory
    {
        public enum ePotionType { HP, MP, END }
        public int[] potion = new int[(int)ePotionType.END];

        public List<Item> list = new List<Item>();
        
        public Item? equipWeapon = null;
        public Item? equipArmor = null;

        Json json;

        public Inventory()
        {
            json = new Json("Inventory.json");

            for (int i = 0; i < potion.Length; i++) { potion[i] = 3; }
        }

        public void Free()
        {
            json.Save(this);
        }

        public void LoadInven()
        {
            Inventory inven = json.Load<Inventory>();

            if (inven == null)
                return;

            for (int i = 0; i < potion.Length; i++)
            {
                potion[i] = inven.potion[i];
            }

            foreach (Item item in inven.list)
            {
                this.list.Add(item);

                if (item.IsEquip)
                {
                    if (item.Type == "weapon") { equipWeapon = item; }
                    else { equipArmor = item; }
                }
            }
        }

        public static void WriteTyping(string str)
        {
            foreach (char c in str)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
            Console.WriteLine();
        }

        public void InventoryScreen()   // 인벤토리 리스트
        {
            Console.Clear();

            WriteTyping("\n인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");

            WriteTyping($"[아이템 목록]");

            if (list.Count == 0)
            {
                Console.WriteLine("");
            }
            else
            {
                int i = 1;
                foreach (Item item in list)
                {
                    if (item.IsEquip == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"-  [E] {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Info}");
                        Console.ResetColor();
                    }
                    else Console.WriteLine($"-  {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Info}");

                    i++;
                }
            }

            WriteTyping("\n1. 장착 관리\n0. 나가기\n");

            int input = CommonUtil.CheckInput(0, 1);

            switch (input)
            {
                case (0):
                    break;

                case (1):
                    EquipItemScreen();
                    break;
            }
        }

        public void EquipItemScreen() // 장착 관리 아이템 리스트 출력
        {
            Console.Clear();
            Console.WriteLine("\n인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine($"[아이템 목록]");

            if (list.Count == 0)
            {
                Console.WriteLine("");
            }
            else
            {
                int i = 1;
                foreach (Item item in list)
                {
                    if (item.IsEquip == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"- {i} [E] {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Info}");
                        Console.ResetColor();
                    }
                    else Console.WriteLine($"- {i} {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Info}");
                    i++;
                }
            }

            Console.WriteLine("\n0. 나가기\n");

                int input = CommonUtil.CheckInput(0, list.Count);

                if (input == 0)
                {
                    InventoryScreen();
                }
                else
                {
                    EquipItem(input, GameManager.Instance.player);
                    EquipItemScreen();
                }
        }

        public void ConsumableScreen() // 소비 아이템 출력창
        {
            Player player = GameManager.Instance.player;

            while (true)
            {
                Console.Clear();
                CommonUtil.WriteLine("회복", ConsoleColor.DarkYellow);
                Console.WriteLine("포션을 사용하면 체력/마력을 30 회복 할 수 있습니다.");
                Console.WriteLine();

                Console.WriteLine($"체력: {player.hp} / {player.maxHp}");
                Console.WriteLine($"마력: {player.mp} / {player.maxMp}");
                Console.WriteLine();

                Console.WriteLine($"1. HP포션 사용하기 (남은 포션: {potion[(int)ePotionType.HP]})");
                Console.WriteLine($"2. MP포션 사용하기 (남은 포션: {potion[(int)ePotionType.MP]})");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();

                int input = CommonUtil.CheckInput(0, 2);

                if (input == 0)
                    return;

                UsePotion((ePotionType)(input - 1));
            }
        }

        private void UsePotion(ePotionType eType)
        {
            Player player = GameManager.Instance.player;

            switch (eType)
            {
                case ePotionType.HP:
                    {
                        if (potion[(int)ePotionType.HP] <= 0)
                        {
                            CommonUtil.WriteLine("포션이 부족합니다.", ConsoleColor.Red);
                            break;
                        }
                        else
                        {
                            potion[(int)ePotionType.HP]--;

                            player.hp += 30;
                            if (player.hp >= player.maxHp)
                                player.hp = player.maxHp;

                            CommonUtil.WriteLine("회복을 완료했습니다.", ConsoleColor.Cyan);
                        }
                    }
                    break;
                case ePotionType.MP:
                    {
                        if (potion[(int)ePotionType.MP] <= 0)
                        {
                            CommonUtil.WriteLine("포션이 부족합니다.", ConsoleColor.Red);
                            break;
                        }
                        else
                        {
                            potion[(int)ePotionType.MP]--;
                            player.mp += 30;
                            if (player.mp >= player.maxMp)
                                player.mp = player.maxMp;

                            CommonUtil.WriteLine("회복을 완료했습니다.", ConsoleColor.Cyan);
                        }
                    }
                    break;
            }

            Thread.Sleep(500);
        }

        public void EquipItem(int index, Player player) // 아이템 장착
        {
            Item item = list[index - 1];

           if (item.Type == "weapon")
            {
                if (equipWeapon != null) // 이미 장착 중인 무기가 있을 경우
                {
                    equipWeapon.IsEquip = false;

                    if (equipWeapon.Equals(item)) // 장착 중인 무기와 선택된 무기가 같을 경우
                    {
                        equipWeapon = null; // 장착 해제
                        return;
                    }
                }
                
                equipWeapon = item;
                item.IsEquip = true;
            }
            else if (item.Type == "armor")
            {
                if (equipArmor != null)
                {
                    equipArmor.IsEquip = false;

                    if (equipArmor.Equals(item))
                    {
                        equipArmor = null;
                        return;
                    }
                }

                equipArmor = item;
                item.IsEquip = true;
            }

            //else if (item.IsEquip == false)
            //{
            //    foreach (Item i in list)
            //    {
            //        if (i.Type == item.Type && i.IsEquip == true)   // 장비 아이템의 타입이 같을 시 기존 장비 해제 후 장착
            //        {
            //            i.IsEquip = false;
            //            i.EquipOption(player);
            //            break;
            //        }
            //    }
            //    item.IsEquip = true;
            //}
            //else
            //{
            //    item.IsEquip = false;
            //}

            //if (player.hp >= player.maxHp)  // 현재 체력의 상한선 = 최대 체력
            //{
            //    player.hp = player.maxHp;
            //}

            //if (player.mp >= player.maxMp)  // 현재 마력의 상한선 = 최대 마력
            //{
            //    player.mp = player.maxMp;
            //}
        }
    }
}
