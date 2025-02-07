using _16TeamTextRPG;
using System.Numerics;

namespace Class_shop_DS_ver._6._0
{
    internal class Program
    {
        static void Main()
        {
            shop.ShowItems(); //메뉴에서 상점으로 이동 후 상점에서 구매하기 전 실행되는 함수
            Console.Write("필요한 아이템을 얻을 수 있는 상점입니다");
            if (int.TryParse(Console.ReadLine(), out int shopItemIndex))
            {
                shopItemIndex.BuyItem(player, shopItemIndex); //상점에서 아이템을 샀을 때 실행되는 함수
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
            }
        }
    }
    class Item
    {
        public string Name { get; set; } // 아이템 이름
        public string Type { get; set; } // 아이템 타입
        public int Attack { get; set; } // 아이템 공격력
        public int Defense { get; set; } // 아이템 방어력
        public string Explanation { get; set; } // 아이템 설명
        public int Price { get; set; } // 아이템 가격
        // set;은 장착할 경우 실행되는 것
        public Item(string name, string type, int attack, int defense, string explanation, int price)
        {
            Name = name;
            Type = type;
            Defense = defense;
            Explanation = explanation;
            Price = price;
        }

        class shop
        {
            public List<Item> ItemForSale { get; set; } = new List<Item>();

            public shop()
            {
                ItemForSale.Add(new Item("낡은 검", "Weapon", 2, 0, "수련에 도움을 주는 갑옷입니다.", 600));
                ItemForSale.Add(new Item("청동 도끼", "Weapon", 5, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", 600));
                ItemForSale.Add(new Item("스파르타의 창", "Weapon", 7, 0, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 600));
                ItemForSale.Add(new Item("수련자 갑옷", "Weapon", 2, 5, "쉽게 볼 수 있는 낡은 검 입니다.", 1000));
                ItemForSale.Add(new Item("무쇠 갑옷", "Weapon", 0, 9, "어디선가 사용됐던거 같은 도끼입니다.", 2000));
                ItemForSale.Add(new Item("스파르타의 갑옷", "Weapon", 2, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3500));
            }
            public void showItem()
            {
                Console.WriteLine("\n필요한 아이템을 얻을 수 있는 상점입니다");
                for (int i = 0; i < ItemForSale.Count; i++)
                {
                    var item = ItemForSale[i];
                    Console.WriteLine($"{i + 1}. {item.Name} (공격력: {item.Attack} || 방어력: {item.Defense} || 설명: {item.Explanation}) || {item.Price} G");
                }
                Console.WriteLine();
            }

            public void BuyItem(Character player, int itemIndex)
            {
                if (itemIndex < 1 || itemIndex > ItemsForSale.Count)
                {
                    Console.WriteLine("잘못된 선택입니다.");
                    return;
                }

                foreach (var ownedItem in player.Inventory)
                {
                    if (ownedItem.Name == itemToBuy.Name)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                        return;
                    }
                }
                if (player.Gold >= itemToBuy.Price) // 골드가 충분하면 구매가능
                {
                    player.Gold -= itemToBuy.Price;
                    player.Inventory.Add(itemToBuy);
                    player.EquipItem(itemToBuy); // 아이템 구매 후 자동으로 장착
                    Console.WriteLine($"{itemToBuy.Name}을(를) 구매하고 자동으로 장착했습니다!");
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
        }
    }
}