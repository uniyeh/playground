using System;
using System.Text;
using System.Collections.Generic;

namespace ChatCoreTest
{
    public class ReadPackage
    {
        private static int packetId;
        private static int pPos;
        public static int result1;
        public static float result2;
        public static string result3;

        public ReadPackage()
        {
        }

        public static void ReadPacket(byte[] packetData)
        {
            Console.WriteLine("enter readpacket");
            pPos = 0;
            List<byte[]> byteResults;
            byteResults = Unpack(packetData);

            Console.WriteLine($"byteReults length:{byteResults.Count}");

            Read(byteResults);
            
            Console.WriteLine($"{result1}, {result2}, {result3}");
            Console.WriteLine("uppack success");
        }

        private static List<byte[]> Unpack(byte[] packetData)
        {
            Console.WriteLine("unpack start");
            List<byte[]> results = new List<byte[]>();

            packetId = ReadInt(packetData, 0, 4);
            pPos += 4;
            Console.WriteLine("id:" + packetId);

            //result[0] => int
            int count = 0;
            byte[] resultI = new byte[4];

            for (int i = pPos; i < pPos + 4; i++)
            {
                resultI[count] = packetData[i];
                count++;
            }

            results.Add(resultI);
            pPos += 4;

            //result[1] => float
            count = 0;
            byte[] resultF = new byte[4];

            for (int i = pPos; i < pPos + 4; i++)
            {
                resultF[count] = packetData[i];
                count++;
            }

            results.Add(resultF);
            pPos += 4;

            //result[2] => string
            while (pPos < packetId)
            {
                int pSize = ReadInt(packetData, pPos, 4);
                pPos += 4;
                Console.WriteLine("pPos:" + pPos);
                Console.WriteLine("pSize:" + pSize);

                count = 0;
                byte[] resultS = new byte[pSize];

                for (int i = pPos; i < pPos + pSize; i++)
                {
                    resultS[count] = packetData[i];
                    count++;    
                }

                results.Add(resultS);
                pPos += pSize;
                Console.WriteLine("pPos2:" + pPos);
            }

            Console.WriteLine("unpack end");
            foreach(byte[] bytes in results)
            {
                for(int i = 0; i < bytes.Length; i++)
                {
                    Console.Write(bytes[i] + ",");
                }
                Console.WriteLine();
            }
            return results;
        }

        private static void Read(List<byte[]> results)
        {
            Console.WriteLine("enter read");

            foreach(byte[] result in results)
            {
                Array.Reverse(result);
            }

            result1 = BitConverter.ToInt32(results[0], 0);
            result2 = BitConverter.ToSingle(results[1], 0);
            result3 = Encoding.Unicode.GetString(results[2]);
           
            Console.WriteLine("read end");
        }

        private static int ReadInt(byte[] packetData, int start, int length)
        {
            Console.WriteLine("enter readInt");

            byte[] intArray = new byte[length];

            int j = start;

            for (int i = 0; i < length; i++)
            {
                intArray[i] = packetData[j];
                j++;
            }

            Array.Reverse(intArray);

            int result = BitConverter.ToInt32(intArray, 0);
            Console.WriteLine("readint end");
            return result;
            
        }
    }
}
