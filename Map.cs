using System;
using System.Collections;
using System.Collections.Generic;
namespace Assignment6
{
    public class Map : IEnumerable
    {
        public object[,] map;

        public Map(int w, int h)
        {
            map = new object[w, h];

            for(int x = 0; x < w; x++)
            {
                for(int y = 0; y > h; y++)
                {
                    map[x, y] = null;
                }
            }
        }

        //Fill in monsters or item or null with position info, player initial position[0,0] is null
        public void FillIn(int monAmt, int bombAmt, int potionAmt)
        {
            Random posX = new Random();
            Random posY = new Random();
            int monOnMap = 0;
            int bombOnMap = 0;
            int potionOnMap = 0;

            //Fill in Monster
            for (int i = 0; i < map.Length; i++)
            {
                int x = posX.Next(1, map.GetLength(0));//Generate random position x
                int y = posY.Next(1, map.GetLength(1));//Generate random position y

                if (monOnMap == monAmt)
                {
                    break;
                }
                else
                {
                    if (map[x, y] == null)
                    {
                        Monster mon = new Monster();
                        map[x, y] = mon;
                        monOnMap += 1;
                        Console.WriteLine("AddMon");
                    }
                }
            }

            //Fill in Bomb
            for(int i = 0; i < map.Length; i++)
            {
                int x = posX.Next(1, map.GetLength(0));//Generate random position x
                int y = posY.Next(1, map.GetLength(1));//Generate random position y

                if (bombOnMap == bombAmt)
                {
                    break;
                }
                else
                {
                    if (map[x, y] == null)
                    {
                        Item bomb = new Item(Item.bomb);
                        map[x, y] = bomb;
                        bombOnMap += 1;
                        Console.WriteLine("AddBomb");
                    }
                }
             
            }

            //Fill in Potion
            for(int i = 0; i < map.Length; i++)
            {
                int x = posX.Next(1, map.GetLength(0));//Generate random position x
                int y = posY.Next(1, map.GetLength(1));//Generate random position y

                if (potionOnMap == potionAmt)
                {
                    break;
                }
                else
                {
                    if (map[x, y] == null)
                    {
                        Item potion = new Item(Item.potion);
                        map[x, y] = potion;
                        potionOnMap += 1;
                        Console.WriteLine("AddPotion");
                    }
                }
                
            }   

            //Fill in int 0 while null, prevents "NullReferenceException"
            for(int i = 0; i < map.GetLength(0); i++)
            {
                int a = map.GetLength(1);
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    if(map[i, j] == null)
                    {
                        map[i, j] = 0;
                        Console.WriteLine("Set0");
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public MapEnum GetEnumerator()
        {
            return new MapEnum(map);
        }
    }

    //Implement IEnumerator of Map
    public class MapEnum : IEnumerator
    {
        public object[,] map;

        //player initial position
        int posX = 0;
        int posY = 0;

        public MapEnum(object[,] objList)
        {
            map = objList;
        }

        //move on x dir
        public bool MoveNext()
        {
            posX += 1;
            Console.WriteLine($"{posX}, {posY}");
            return (posX < map.GetLength(0) && posY < map.GetLength(1));
        }

        //move on y dir
        public bool MoveDown()
        {
            posY += 1;
            Console.WriteLine($"{posX}, {posY}");
            return (posX < map.GetLength(0) && posY < map.GetLength(1));
        }

        public void Reset()
        {
            posX = 0;
            posY = 0;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public object Current
        {
            get
            {
                try
                {
                    return map[posX, posY];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

    }
}
