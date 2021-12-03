using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Assignment6
{
    class Program
    {
        public static Player player;
        public static Mission mission;
        public static MapEnum mapEnum;
        public Monster taMon;
        public Dictionary<string, Action<int>> _pActions = new Dictionary<string, Action<int>>();
        public static Program program;

        static void Main()
        {
            player = new Player();
            mission = new Mission();
            mapEnum = mission.map.GetEnumerator();
            program = new Program();


            Console.WriteLine("開始遊戲！");
            Console.WriteLine($"任務目標：從(0,0)走到({mission.mapW},{mission.mapH})！每次可以選擇x+1或y+1。");

            while (true)
            {
                Console.WriteLine("0 => 結束遊戲; 1 => 顯示玩家狀態; D => x+1; S => y+1");
                string keyin = Console.ReadLine().ToUpper();//player enter key
                
                if (program._pActions.ContainsKey(keyin) == true && (keyin == "0" || keyin == "1" || keyin == "D" || keyin == "S"))
                {
                    Action<int> pAct = program._pActions[keyin];
                    pAct(0);
                }
                else
                {
                    Console.WriteLine("請輸入正確指令");
                }

                //Mission fail
                if (mission.FailedOrNot(player))
                {
                    Console.WriteLine("關卡失敗");
                    break;
                }

                //Mission success
                if (mission.SuccessOrNot(player))
                {
                    Console.WriteLine("成功通關！");
                    break;
                }
            }
        }

        public Program()
        {
            _pActions.Add("S", KeyS());
            _pActions.Add("D", KeyD());
            _pActions.Add("1", Key1());
            _pActions.Add("2", Key2());
            _pActions.Add("3", Key3());
            _pActions.Add("4", Key4());
            _pActions.Add("0", Key0());

        }

        //y+1
        Action<int> KeyS()
        {
            return num => YAddOne(num);
        }

        //x+1
        Action<int> KeyD()
        {
            return num => XAddOne(num);
        }

        //Show player data
        Action<int> Key1()
        {
            return num => PrintPlayerData(num);
        }

        //Attack Monster
        Action<int> Key2()
        {
            return num => AttackMon(num);
        }

        //Use Bomb
        Action<int> Key3()
        {
            return num => UseBomb(num);
        }

        //Use Potion
        Action<int> Key4()
        {
            return num => UsePotion(num);
        }

        Action<int> Key0()
        {
            return num => QuitGame(num);
        }

        public void YAddOne(int i)
        {
            player.y += 1;

            Console.WriteLine("Y移動了一格");
            Console.WriteLine($"{player.x}, {player.y}");
            
            while (mapEnum.MoveDown())
            {
                if (mapEnum.Current.GetType() == typeof(Monster))
                {
                    taMon = (Monster)mapEnum.Current;
                    Console.WriteLine("你遭遇了怪物！");
                    taMon.PrintData();
                }
                else if (mapEnum.Current.GetType() == typeof(Item))
                {
                    Item itemGot = (Item)mapEnum.Current;
                    player.bag.bagArr = player.bag.AddToBag(itemGot);
                    string itemName = itemGot.GetName();
                    Console.WriteLine("你獲得了" + itemName);
                }
                else
                {
                    Console.WriteLine("一片荒涼⋯⋯");
                }
            }
        }

        public void XAddOne(int i)
        {
            player.x += 1;

            Console.WriteLine("X移動了一格");
            Console.WriteLine($"{player.x}, {player.y}");
            
            while (mapEnum.MoveDown())
            {
                if (mapEnum.Current.GetType() == typeof(Monster))
                {
                    taMon = (Monster)mapEnum.Current;
                    Console.WriteLine("你遭遇了怪物！");
                    taMon.PrintData();
                    EnterFight(taMon);
                }
                else if (mapEnum.Current.GetType() == typeof(Item))
                {
                    Item itemGot = (Item)mapEnum.Current;
                    player.bag.bagArr = player.bag.AddToBag(itemGot);
                    string itemName = itemGot.GetName();
                    Console.WriteLine("你獲得了" + itemName);
                }
                else
                {
                    Console.WriteLine("一片荒涼......");
                }
            }
        }

        public void PrintPlayerData(int i)
        {
            player.PrintPlayerGroupData();
        }

        public void AttackMon(int i)
        {
            int orignState = taMon.state;
            Item dontUseItem = new Item(Item.none);
            player.GroupAttackandPrintResult(dontUseItem, taMon, player);
            mission.GalcRoundResultAndPrint(taMon, player, taMon.MonStateChangeOrNot(orignState));
        }

        public void UseBomb(int i)
        {
            Item item = new Item(Item.bomb);
            int orignState = taMon.state;
            player.GroupAttackandPrintResult(item, taMon, player);
            player.hp = taMon.AttackandPrintResult(player);
            mission.GalcRoundResultAndPrint(taMon, player, taMon.MonStateChangeOrNot(orignState));
        }

        public void UsePotion(int i)
        {
            Item item = new Item(Item.potion);
            int orignState = taMon.state;
            player.GroupAttackandPrintResult(item, taMon, player);
            player.hp = taMon.AttackandPrintResult(player);
            mission.GalcRoundResultAndPrint(taMon, player, taMon.MonStateChangeOrNot(orignState));
        }
        
        public void QuitGame(int i)
        {
            Console.WriteLine("確定結束遊戲？\nYes => Press 0; No => Press 1");
            string checkQuit = Console.ReadLine();//player enter key
            bool quitgame = int.TryParse(checkQuit, out int checkQuitToInt);
            if (quitgame)
            {
                if (checkQuitToInt == 0)//quit game
                {
                    Console.WriteLine("結束遊戲");
                    Environment.Exit(0);
                }
                else if (checkQuitToInt == 1)
                {
                    Console.WriteLine("繼續遊戲");
                }
                else
                {
                    Console.WriteLine("請輸入正確指令");
                }
            }
        }

        public void EnterFight(Monster mon)
        {
            taMon = mon;

            while (taMon.hp != 0)
            {
                Console.WriteLine("2 => 攻擊; 3 => 使用炸彈; 4 => 使用藥水");
                string keyin = Console.ReadLine().ToUpper();//player enter key

                if (program._pActions.ContainsKey(keyin) == true && (keyin == "2" || keyin == "3" || keyin == "4"))
                {
                    Action<int> pAct = program._pActions[keyin];
                    pAct(0);
                }
                else
                {
                    Console.WriteLine("請輸入正確指令");
                }
            }
        }
    }
}
