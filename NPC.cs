using System;
namespace Assignment6
{
    abstract public class NPC
    {
        public int maxHp;
        private int totalexp;
        public int Exp
        {
            set { totalexp = Exp; }
            get { return totalexp % 5; }
        }

        public int Lv
        {
            get { return totalexp / 5; }
        } 

        public int dex;
        public int st;
        public int hp;

        public int fullHp = 0;
        public int bloodLost = 1;
        public int dead = 2;
        public int state;

        public NPC()
        {
            state = fullHp;
        }

        //NPC Skill
        public abstract int ActivateSkillAndPrintResult(Monster mon, Player player);

        //Show NPC data
        public abstract void PrintData();

        //Update NPC status/state
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

 
    //玩家夥伴：騎士
    public class Knight : NPC
    {
        public Knight()
        {
            maxHp = (int)Config.Knight.maxHp;
            Exp = (int)Config.Knight.exp;
            dex = (int)Config.Knight.dex;
            st = (int)Config.Knight.st;
            hp = maxHp;
        }

        //騎士技能
        public override int ActivateSkillAndPrintResult(Monster mon, Player player)
        {
            if (state != dead)
            {
                if (dex * 2 > mon.dex)
                {
                    mon.hp -= st;
                    mon.hp = Math.Max(mon.hp, 0);
                    Console.WriteLine($"騎士攻擊傷害：{st}");
                }
                else
                {
                    Console.WriteLine("MISS!");
                }
            }

            return mon.hp;
        }

        //顯示騎士狀態
        public override void PrintData()
        {
            Console.WriteLine($"夥伴一：騎士\n血量：{hp};力量：{st};敏捷：{dex};等級：{Lv};經驗：{Exp}");
        }
    }

    public class Monk : NPC
    {
        public Monk()
        {
            maxHp = (int)Config.Monk.maxHp;
            Exp = (int)Config.Monk.exp;
            dex = (int)Config.Monk.dex;
            st = (int)Config.Monk.st;
            hp = maxHp;
        }

        //僧侶技能
        public override int ActivateSkillAndPrintResult(Monster mon, Player player)
        {
            if (state != dead)
            {
                player.hp += st;
                player.hp = Math.Min(player.hp, player.maxHp);
                Console.WriteLine($"祭司祝福！玩家血量＋{st}");
            }

            return player.hp;
        }

        //顯示僧侶狀態
        public override void PrintData()
        {
            Console.WriteLine($"夥伴二：祭司\n血量：{hp};力量：{st};敏捷：{dex};等級：{Lv};經驗：{Exp}");
        }
    }
}