using System;
using System.Text;

namespace ChatCoreTest
{
  internal class Program
  {
    private static byte[] m_PacketData;
    private static uint m_Pos;
    private static byte[] p_ID;

    public static void Main(string[] args)
    {
      m_PacketData = new byte[1024];
      m_Pos = 4;

      WriteIntToBArray(109);
      WriteFloatToBArray(109.99f);
      WriteStringToBArray("Hello!");

      p_ID = BitConverter.GetBytes(m_Pos);

      if (BitConverter.IsLittleEndian)
      {
          Array.Reverse(p_ID);
      }
      p_ID.CopyTo(m_PacketData, 0);

      Console.Write($"Output Byte array(length:{m_Pos}): ");
      for (var i = 0; i < m_Pos; i++)
      {
        Console.Write(m_PacketData[i] + ", ");
      }

      ReadPackage.ReadPacket(m_PacketData);
    }

    // write an integer into a byte array
    private static bool WriteIntToBArray(int i)
    {
      // convert int to byte array
      var bytes = BitConverter.GetBytes(i);
      WriteInPacket(bytes);
      return true;
    }

    // write a float into a byte array
    private static bool WriteFloatToBArray(float f)
    {
      // convert int to byte array
      var bytes = BitConverter.GetBytes(f);
      WriteInPacket(bytes);
      return true;
    }

    // write a string into a byte array
    private static bool WriteStringToBArray(string s)
    {
      // convert string to byte array
      var bytes = Encoding.Unicode.GetBytes(s);

      // write byte array length to packet's byte array
      if (WriteIntToBArray(bytes.Length) == false)
      {
        return false;
      }

      WriteInPacket(bytes);
      return true;
    }

    // write a byte array into packet's byte array
    private static void WriteInPacket(byte[] byteData)
    {
      //converter little-endian to network's big-endian
      if (BitConverter.IsLittleEndian)
      {
        Array.Reverse(byteData);
      }

      byteData.CopyTo(m_PacketData, m_Pos);
      m_Pos += (uint)byteData.Length;
    }
  }
}
