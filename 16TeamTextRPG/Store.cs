using _16TeamTextRPG;
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
            ShowItem(); //메뉴에서 상점으로 이동 후 상점에서 구매하기 전 실행되는 함수
            Console.Write("필요한 아이템을 얻을 수 있는 상점입니다");

            if (int.TryParse(Console.ReadLine(), out int shopItemIndex))
            {
                BuyItem(player, shopItemIndex); //상점에서 아이템을 샀을 때 실행되는 함수
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
            }
        }

        public void ShowItem()
        {
            Console.WriteLine("\n필요한 아이템을 얻을 수 있는 상점입니다");
            for (int i = 0; i < ItemForSale.Count; i++)
            {
                var item = ItemForSale[i];
                Console.WriteLine($"{i + 1}. {item.Name} (공격력: {item.Atk} || 방어력: {item.Def} || 설명: {item.Option}) || {item.Price} G");
            }
            Console.WriteLine();
        }

        public void BuyItem(Player player, int itemIndex)
        {
            //if (itemIndex < 1 || itemIndex > ItemsForSale.Count)
            //{
            //    Console.WriteLine("잘못된 선택입니다.");
            //    return;
            //}

            //foreach (var ownedItem in player.Inventory)
            //{
            //    if (ownedItem.Name == itemToBuy.Name)
            //    {
            //        Console.WriteLine("이미 구매한 아이템입니다.");
            //        return;
            //    }
            //}
            //if (player.Gold >= itemToBuy.Price) // 골드가 충분하면 구매가능
            //{
            //    player.Gold -= itemToBuy.Price;
            //    player.Inventory.Add(itemToBuy);
            //    player.EquipItem(itemToBuy); // 아이템 구매 후 자동으로 장착
            //    Console.WriteLine($"{itemToBuy.Name}을(를) 구매하고 자동으로 장착했습니다!");
            //}
            //else
            //{
            //    Console.WriteLine("Gold가 부족합니다.");
            //}
        }
    }
}