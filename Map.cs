using System;
using System.Collections;
using System.Collections.Generic;
namespace Assignment6
{
    public class Map : IEnumerable
    {
        private object[,] map;

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
            int x = posX.Next(1, map.Rank);//Generate random position x
            int y = posY.Next(1, map.GetLength(1));//Generate random position y

            //Fill in Monster
            for(int monOnMap = 0; monOnMap < monAmt; monOnMap++)
            {
                if (map[x, y] == null)
                {
                    Monster mon = new Monster();
                    map[x, y] = mon;
                }
            }

            //Fill in Bomb
            for(int bombOnMap = 0; bombOnMap < bombAmt; bombOnMap++)
            {
                if (map[x, y] == null)
                {
                    map[x, y] = Item.bomb;
                }
            }

            //Fill in Potion
            for(int potionOnMap = 0; potionOnMap < potionAmt; potionOnMap++)
            {
                if(map[x, y] == null)
                {
                    map[x, y] = Item.potion;
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
            posX ++;
            return (posX < map.Length && posY < map.GetLength(posX));
        }

        //move on y dir
        public bool MoveDown()
        {
            posY ++;
            return (posX < map.Length && posY < map.GetLength(posX));
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
