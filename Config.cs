using System;
namespace Assignment6
{
    public class Config
    {
        //小型怪物
        public enum Monster_s
        {
            maxHp = 40,
            exp = 8,
            dex = 30,
            st = 30,
        }

        //中型怪物
        public enum Monster_m
        {
            maxHp = 80,
            exp = 15,
            dex = 45,
            st = 45,
        }

        //大型怪物
        public enum Monster_l
        {
            maxHp = 250,
            exp = 26,
            dex = 80,
            st = 80,
        }

        //騎士
        public enum Knight
        {
            maxHp = 180,
            exp = 0,
            dex = 50,
            st = 20,
        }

        //僧侶
        public enum Monk
        {
            maxHp = 150,
            exp = 0,
            dex = 30,
            st = 10,
        }
    }
}
