using System;
using System.Collections;

namespace Assignment6
{
    public class Mission
    {
        //Claim new map data
        public Map map;
        public int mapW = 4;
        public int mapH = 3;
        public int monAmt = 3;
        public int bombAmt = 3;
        public int potionAmt = 3;

        public Mission()
        {
            if (!InitSuccess())
            {
                Console.WriteLine("Error: Item amt > map size");
            }
            else
            {
                map = new Map(mapW, mapH);
                map.FillIn(monAmt, bombAmt, potionAmt);

                foreach (object obj in map.map)
                {
                    Console.Write( obj.GetType().Name + " ");
                }

            }
        }

        //Check mission set up success or not
        public bool InitSuccess()
        {
            //Item amt > map size
            if ((monAmt + bombAmt + potionAmt) > mapW * mapH)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Mission fail condition: player hp <= 0
        public bool FailedOrNot(Player player)
        {
            return player.hp <= 0;
        }

        //Mission success condition: player arrive position(mapW,mapH)
        public bool SuccessOrNot(Player player)
        {
            if(player.x == mapW && player.y == mapH)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //計算並印出結果
        public void GalcRoundResultAndPrint(Monster mon, Player player, bool monStateChangeOrNot)
        {

            if (mon.state == mon.dead)
            {
                if (monStateChangeOrNot == true)
                {

                    if (monStateChangeOrNot)
                    {
                        player.GainExp(mon);
                    }

                    Console.WriteLine($"怪獸剩餘血量：{mon.hp}");
                    Console.WriteLine($"成功打倒怪獸！\nExp+{mon.exp}");
                    Console.WriteLine($"你的等級:{player.Lv}");
                    Console.WriteLine($"你的經驗值：{player.Exp}");

                }
                else
                {
                    Console.WriteLine("屍體是不會有反應的");
                }
            }
            else
            {
                Console.WriteLine($"怪獸剩餘血量：{mon.hp}");
            }

        }
    }
}
