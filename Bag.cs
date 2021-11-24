using System;
using System.Collections.Generic;
namespace Assignment6
{
    public class Bag
    {
        public Item[] bagArr;

        public Bag(int bagCapcity)
        {
            bagArr = new Item[bagCapcity];
            for(int i = 0; i < bagCapcity; i++)
            {
                bagArr[i] = new Item(Item.none);
            }
        }

        //Show item in bag
        public void PrintBagContent()
        {
            int bombAmt = 0;
            int potionAmt = 0;

            foreach (Item i in bagArr)
            {
                if (i.itemType == Item.bomb)
                {
                    bombAmt ++;
                }
                else if (i.itemType == Item.potion)
                {
                    potionAmt ++;
                }
            }

            Console.WriteLine($"背包：炸彈x{bombAmt / (int)Item.ItemSize.bomb}; 藥水x{potionAmt / (int)Item.ItemSize.potion}") ;
        }

        //Add item to bag
        public Item[] AddToBag(Item itemAdded)
        {
            int noneAmt = 0;
            foreach (Item i in bagArr)
            {
                if(i.itemType == Item.none)
                {
                    noneAmt++;
                }
            }

            int addSize = itemAdded.GetSize();

            if(addSize <= noneAmt)
            {
                for (int i = 0; i < addSize; i++)
                {
                    for (int j = 0; j < bagArr.Length; j++)
                    {
                        if(bagArr[j].itemType == Item.none)
                        {
                            bagArr[j] = itemAdded;
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("背包空間不足");
            }

            return bagArr;
        }

        //Use item in bag
        public Bag UseItem(Player player, Monster mon, Bag bag, Item itemUsed)
        {
            string name = itemUsed.GetName();
            if (AlbleToUseItem(itemUsed.itemType))
            {
                mon.hp = Item.ActivateItemAndPrintResult(player, mon, itemUsed.itemType);
                bag.bagArr = RemoveFromBag(itemUsed);
            }
            else
            {
                Console.WriteLine($"找不到{name}");
            }

            return bag;
        }

        //Able to use item or not
        public bool AlbleToUseItem(int searchType)
        {
            foreach (Item i in bagArr)
            {
                if (searchType == i.itemType)
                {
                    return true;
                }
            }

            return false;
        }

        //Remove item from bag
        public Item[] RemoveFromBag(Item itemRemoved)
        {
            int removeSize = itemRemoved.GetSize();

            for (int i = 1; i == removeSize; i++)
            {
                for (int j = 0; j < bagArr.Length; j++)
                {
                    if (bagArr[j].itemType == Item.none)
                    {
                        bagArr[j] = new Item(Item.none);
                        break;
                    }
                }
            }

            return bagArr;
        }
    }
}
