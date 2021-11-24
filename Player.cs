using System;

namespace Assignment6
{
    public class Player
    {
        private int totalexp = 0;
        public int Exp
        {
            set { Exp = totalexp; }
            get { return totalexp % 5; }
        }

        public int Lv
        {
            set { Lv = Lv; }
            get { return totalexp / 5; }
        }

        public int st = 30;
        public int dex = 50;
        public int maxHp = 260;
        public int hp;

        //position info
        public int x = 0;
        public int y = 0;

        private readonly int bagCapcity = 10;
        public Bag bag;
        public Knight knight;
        public Monk monk;

        public Player()
        {
            hp = maxHp;
            bag = new Bag(bagCapcity);
            knight = new Knight();
            monk = new Monk();
        }

        public void PrintPlayerGroupData()
        {
            Console.WriteLine($"玩家狀態\n血量：{hp};力量：{st};敏捷：{dex};等級：{Lv};經驗：{Exp}");
            bag.PrintBagContent();
            knight.PrintData();
            monk.PrintData();
        }

        public void GroupAttackandPrintResult(Item item, Monster mon, Player player)
        {
            mon.hp = Attack(player, item, mon);
            mon.hp = knight.ActivateSkillAndPrintResult(mon, player);
            player.hp = monk.ActivateSkillAndPrintResult(mon, player);
            mon.state = mon.UpdateState();
        }

        public int Attack(Player player, Item item, Monster mon)
        {
            if (item.itemType != 0)
            {
                bag = bag.UseItem(player, mon, bag, item);
            }
            else if(dex * 2 > mon.dex && mon.hp > 0)
            {
                mon.hp -= st;
                Console.WriteLine($"玩家攻擊傷害：{st}");
            }
            else
            {
                Console.WriteLine("MISS!");
            }

            return mon.hp;
        }

        public void GainExp(Monster mon)
        {
            Exp += mon.exp;
            Lv += mon.exp;
        }
    }
}
