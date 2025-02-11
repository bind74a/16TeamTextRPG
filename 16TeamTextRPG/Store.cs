using _16TeamTextRPG;
using _16TeamTexTRPG;
using System.Numerics;

namespace _16TeamTextRPG
{
    public class Shop
    {
        public List<Item> ItemForSale { get; set; } = new List<Item>();
        Player player;
        
        public Shop()
        {
            //player = GameManager.Instance.player;
            
            foreach( Item item in GameManager.Instance.itemList.all)
            {
                ItemForSale.Add(item);
            }
        }

        public void ShowMain()
        {
            Console.Clear();
            Inventory.WriteTyping("\n상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Inventory.WriteTyping($"[보유골드]\n{GameManager.Instance.player.gold} G");
            Inventory.WriteTyping($"\n[아이템 목록]");

            for (int i = 0; i < ItemForSale.Count; i++)
            {
                var item = ItemForSale[i];
                string price = item.CanBuy == true ? item.Price.ToString() + "G" : "구매 완료";

                Console.WriteLine($"-  {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Info}" + "|" + $"{price} G");
            }
            Console.WriteLine();

            Inventory.WriteTyping("\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n");

            int input = CommonUtil.CheckInput(0, 2);

            switch (input)
            {
                case 0:
                    break;
                case 1: 
                    BuyItemList();
                    break;
                case 2:
                    ShowSellItem();
                    break;
            }
        }

        public void BuyItemList()   // 아이템 구매 리스트 출력
        {
            Console.Clear();
            Console.WriteLine("\n상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($"[보유골드]\n{GameManager.Instance.player.gold} G");
            Console.WriteLine($"\n[아이템 목록]");

            if (ItemForSale.Count == 0)
            {
                Console.WriteLine("");
            }
            else
            {
                int i = 1;
                foreach (Item item in ItemForSale)
                {
                    string price = item.CanBuy == true ? item.Price.ToString() + "G" : "구매 완료";
                    Console.WriteLine($"- {i} {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Info}" + "|" + $"{price} G");
                    i++;
                }
            }

            Console.WriteLine();
            int input = CommonUtil.CheckInput(0, ItemForSale.Count);

            if (input == 0)
            {
                ShowMain();
            }

            else BuyItem(input, GameManager.Instance.player, GameManager.Instance.inventory);
        }

        public void BuyItem(int index, Player status, Inventory inventory) // 아이템 구매
        {
            Item item = ItemForSale[index - 1];
            if (ItemForSale[index - 1].CanBuy == false)
            {
                Console.WriteLine("\n이미 구매한 아이템입니다.");
            }
            else if (status.gold >= ItemForSale[index - 1].Price)
            {
                status.gold -= ItemForSale[index - 1].Price;
                inventory.list.Add(ItemForSale[index - 1]);
                ItemForSale[index - 1].CanBuy = false;
                Console.WriteLine("\n구매를 완료했습니다.");
            }
            else
            {
                Console.WriteLine("\nGold 가 부족합니다.");
            }
            BuyItemList();
        }

        public void ShowSellItem() // 아이템 판매창 출력
        {
            Console.Clear();
            Console.WriteLine("\n상점 - 아이템 판매\n아이템을 판매 할 수 있는 상점입니다.\n");
            Console.WriteLine($"[보유골드]\n{GameManager.Instance.player.gold} G");
            Console.WriteLine($"\n[아이템 목록]");
            SellItemList(GameManager.Instance.inventory);
            Console.WriteLine();
 
            int input = CommonUtil.CheckInput(0, GameManager.Instance.inventory.list.Count);
            
            if (input == 0)
            {
                ShowMain();
            }
            else if (input <= GameManager.Instance.inventory.list.Count)
            {
                Console.Clear();
                SellItem(input, GameManager.Instance.player, GameManager.Instance.inventory);
                ShowSellItem();
            }
        }

        public void SellItemList(Inventory inventory)   // 아이템 판매 리스트 출력
        {
            if (inventory.list.Count == 0)
            {
                Console.WriteLine("");
            }
            else
            {
                int i = 1;
                foreach (Item item in inventory.list)
                {
                    string price = ((int)(item.Price * 0.85f)).ToString() + "G";
                    Console.WriteLine($"- {i} {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Info}" + "|" + $"{price}");
                    i++;
                }
            }
        }

        public void SellItem(int index, Player player, Inventory inventory) // 아이템 판매
        {
            Item item = inventory.list[index - 1];
            inventory.EquipItem(index, player);
            inventory.list.RemoveAt(index - 1);
            ItemForSale[index - 1].CanBuy = true;
            player.gold += (int)(item.Price * 0.85f);
            Console.WriteLine("판매를 완료했습니다.");
        }
    }
}