using System;
namespace Assignment6
{
    public class Item
    {
        public static int none = 0;
        public static int bomb = 1;
        public static int potion = 2;
        public int itemType;

        public enum ItemSize
        {
            none = 0,
            potion = 1,
            bomb = 4,
        }

        public Item(int type)
        {
            itemType = type;
        }

        public int GetSize()
        {
            if(itemType == bomb)
            {
                return (int) ItemSize.bomb;
            }
            else if(itemType == potion)
            {
                return (int)ItemSize.potion;
            }
            else
            {
                return (int)ItemSize.bomb;
            }
        }

        public string GetName()
        {
            string itemName;

            if (itemType == bomb)
            {
                itemName = "炸彈";
            }
            else if (itemType == potion)
            {
                itemName = "藥水";
            }
            else
            {
                itemName = "空格";
            }

            return itemName;
        }

        public static int ActivateItemAndPrintResult(Player player, Monster mon, int findType)
        {
            if (findType == bomb)
            {
                mon.hp -= 100;
                mon.hp = Math.Max(mon.hp, 0);
                Console.WriteLine("炸彈傷害：100");
            }
            else if (findType == potion)
            {
                if(player.hp < player.maxHp)
                {
                    player.hp += 20;
                    Console.WriteLine($"玩家血量+20");
                }
                else
                {
                    Console.WriteLine("現在不是用這個的時候⋯⋯");
                }
            }
            else
            {
                Console.WriteLine("找不到道具");
            }

            return mon.hp;
        }
    }
}
