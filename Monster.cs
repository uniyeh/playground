using System;
namespace Assignment6
{
    public class Monster
    {
        public int maxHp;
        public int exp;
        public int dex;
        public int st;
        public int hp;

        public int fullHp = 0;
        public int bloodLost = 1;
        public int dead = 2;
        public int state;

        /// <summary>
        /// new Monster時，會隨機挑出s.m.l之中的其中一種怪物new
        /// </summary>
        public Monster()
        {
            Random newMon = new Random();
            int montype = newMon.Next(0, 3);

            switch (montype)
            {
                case 0:
                    maxHp = (int)Config.Monster_s.maxHp;
                    exp = (int)Config.Monster_s.exp;
                    dex = (int)Config.Monster_s.dex;
                    st = (int)Config.Monster_s.st;
                    break;

                case 1:
                    maxHp = (int)Config.Monster_m.maxHp;
                    exp = (int)Config.Monster_m.exp;
                    dex = (int)Config.Monster_m.dex;
                    st = (int)Config.Monster_m.st;
                    break;

                case 2:
                    maxHp = (int)Config.Monster_l.maxHp;
                    exp = (int)Config.Monster_l.exp;
                    dex = (int)Config.Monster_l.dex;
                    st = (int)Config.Monster_l.st;
                    break;
            }

            hp = maxHp;
            state = fullHp;
        }

        //怪物技能：攻擊玩家方全體成員
        public int AttackandPrintResult(Player player)
        {
            if (state != dead)
            {
                AttackPlayer(player);
                player.knight.hp = AttackKnight(player.knight);
                player.knight.state = player.knight.UpdateState();
                player.monk.hp = AttackMonk(player.monk);
                player.monk.state = player.monk.UpdateState();
            }

            return player.hp;
        }

        //攻擊玩家
        public int AttackPlayer(Player player)
        {
            if (dex > player.dex)
            {
                player.hp -= st;
                player.hp = Math.Max(player.hp, 0);
                Console.WriteLine($"玩家血量 -{st}");
            }
            else
            {
                Console.WriteLine("攻擊玩家MISS!");
            }

            return player.hp;
        }

        //攻擊騎士
        public int AttackKnight(Knight knight)
        {
            if (dex > knight.dex && knight.hp > 0)
            {
                knight.hp -= st;
                knight.hp = Math.Max(knight.hp, 0);
                Console.WriteLine($"騎士血量 -{st}");
            }
            else
            {
                Console.WriteLine("攻擊騎士MISS!");
            }

            return knight.hp;
        }

        //攻擊僧侶
        public int AttackMonk(Monk monk)
        {
            if (dex > monk.dex && monk.hp > 0)
            {
                monk.hp -= st;
                monk.hp = Math.Max(monk.hp, 0);
                Console.WriteLine($"祭司血量 -{st}");
            }
            else
            {
                Console.WriteLine("攻擊祭司MISS!");
            }

            return monk.hp;
        }

        //顯示怪物資訊，狀態轉為中文字串
        public void PrintData()
        {
            string str;

            if (state == fullHp)
            {
                str = "滿血";
            }
            else if (state == bloodLost)
            {
                str = "損血";
            }
            else
            {
                str = "死亡";
            }

            Console.WriteLine($"怪物資訊\n血量：{hp};攻擊力：{st};狀態：{str}");
        }

        //檢查怪物狀態是否有變化
        public bool MonStateChangeOrNot(int originState)
        {
            return originState != state;
        }

        public int UpdateState()
        {
            if (hp <= 0)
            {
                state = dead;
            }
            else if (hp == maxHp)
            {
                state = fullHp;
            }
            else
            {
                state = bloodLost;
            }

            return state;
        }
    }
}
