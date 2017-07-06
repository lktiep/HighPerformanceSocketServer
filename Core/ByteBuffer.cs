using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	public class ByteBuffer : IDisposable
	{
		public readonly bool IsLittleEndian = true;

		//private const int MAXLENGTH = 1024;
		private List<byte> Data;
		//private int _length;
		//private int maxLength;
		private int index;

		#region Properties
		public int Index => index;

		public int Length => Data.Count;

		#endregion

		#region Constructor

		public ByteBuffer()
		{
			Data = new List<byte>();
			index = -1;

		}

		public ByteBuffer(int length)
		{
			Data = new List<byte>(length);
			index = -1;
		}

		public ByteBuffer(IEnumerable<byte> buff)
		{
			Data = new List<byte>(buff);
			index = -1;
		}

		#endregion
		public void ResetIndex()
		{
			index = 0;
		}

		#region Reading data

		public short ReadInt16()
		{
			if (index > Length - 2)
			{
				throw new IndexOutOfRangeException("ReadInt16");
			}

			index += 2;
			return (short)(Data[index - 1] | Data[index] << 8);
		}

		public ushort ReadUInt16()
		{
			if (index > Length - 2)
			{
				throw new IndexOutOfRangeException("ReadUInt16");
			}

			return (ushort)ReadInt16();
		}

		public int ReadInt32()
		{
			if (index > Length - 4)
			{
				throw new IndexOutOfRangeException("ReadInt32");
			}

			index += 4;
			return ((Data[index - 3] | Data[index - 2] << 8) | Data[index - 1] << 16) | Data[index] << 24;
		}

		public uint ReadUInt32()
		{
			if (index > Length - 4)
			{
				throw new IndexOutOfRangeException("ReadUInt32");
			}

			return (uint)ReadInt32();
		}

		public long ReadInt64()
		{
			if (index > Length - 8)
			{
				throw new IndexOutOfRangeException("ReadInt64");
			}

			int num = ReadInt32();
			int num2 = ReadInt32();
			return (long)num2 | num << 0x20;
		}

		public ulong ReadUInt64()
		{
			if (index > Length - 8)
			{
				throw new IndexOutOfRangeException("ReadUInt64");
			}

			return (ulong)ReadInt64();
		}

		public unsafe float ReadSingle()
		{
			if (index > Length - 4)
			{
				throw new IndexOutOfRangeException("ReadSingle");
			}

			var temp = ReadInt32();
			return *(((float*)&temp));
		}

		public unsafe double ReadDouble()
		{
			if (index > Length - 8)
			{
				throw new IndexOutOfRangeException("ReadDouble");
			}

			var temp = ReadInt32();
			return *(((double*)&temp));
		}

		public byte ReadByte()
		{
			if (index > Length - 1)
			{
				throw new IndexOutOfRangeException("ReadByte");
			}
			index += 1;
			return Data[index];
		}

		public byte[] ReadByte(int length)
		{
			if (index > Length - length)
			{
				throw new IndexOutOfRangeException("ReadByte");
			}

			var data = Data.GetRange(index + 1, length).ToArray();
			index += length;
			return data;
		}

		public string ReadString(int count)
		{
			if (index > Length - count)
			{
				throw new IndexOutOfRangeException("ReadString");
			}

			var data = Data.GetRange(index + 1, count).ToArray();
			index += count;

			// Find first empty byte (0x00)
			var i = 0;
			for (int j = 0; j < data.Length; j++)
			{
				i = j;
				if (data[j] == 0x00)
				{

					break;
				}
			}

			// Get String den vi tri i
			var str = Encoding.GetEncoding(1258).GetString(data).Replace("\0", "");
			return str;
		}

		/// <summary>
		/// Đọc chuỗi String cho đến cuối mảng
		/// </summary>
		/// <returns></returns>
		public string ReadString()
		{
			if (index > Length)
			{
				throw new IndexOutOfRangeException("ReadString");
			}

			return ReadString(Length - index);
		}

		public short ReadShort()           // Read Hex
		{
			if (index >= Length - 2)
			{
				var temp = (short)(Data[index + 1] << 8 | Data[index]);
				index += 2;
				return temp;
			}
			throw new OverflowException("Index out of range");
		}


		#endregion

		#region Write data

		public void WriteB(byte value)
		{
			WriteByte(value);
		}

		public void WriteB(byte[] value, int length)
		{
			WriteByte(value, length);
		}

		public void WriteB(byte[] value, int start, int length)
		{
			WriteByte(value, start, length);
		}

		public void WriteC(byte value)
		{
			WriteInt16(value & 0xFF);
		}

		public void WriteH(short value)
		{
			WriteInt16(value);
		}

		public void WriteH(ushort value)
		{
			WriteUInt16(value);
		}

		public void WriteD(int value)
		{
			WriteInt32(value);
		}

		public void WriteD(uint value)
		{
			WriteUInt32(value);
		}

		public void WriteQ(long value)
		{
			WriteInt64(value);
		}

		public void WriteQ(ulong value)
		{
			WriteUInt64(value);
		}

		public void WriteF(float value)
		{
			WriteSingle(value);
		}

		public void WriteDf(double value)
		{
			WriteDouple(value);
		}

		public void WriteInt16(short value)
		{
			//if (index > Length - 2)
			//{
			//    throw new IndexOutOfRangeException("WriteInt16");
			//}

			byte[] tmp = BitConverter.GetBytes(value);
			Data.AddRange(tmp);

			//Data[index] = tmp[0];
			//Data[index + 1] = tmp[1];

			//index += 2;
		}

		public void WriteInt16(int value)
		{
			WriteInt16((short)value);
		}

		public void WriteUInt16(ushort value)
		{
			//if (index > Length - 2)
			//{
			//    throw new IndexOutOfRangeException("WriteUInt16");
			//}

			byte[] tmp = BitConverter.GetBytes(value);
			Data.AddRange(tmp);

			//Data[index] = tmp[0];
			//Data[index + 1] = tmp[1];

			//index += 2;
		}

		public void WriteInt32(int value)
		{
			//if (index > Length - 4)
			//{
			//    throw new IndexOutOfRangeException("WriteInt32");
			//}

			//byte[] tmp = BitConverter.GetBytes(value);
			//Data.AddRange(tmp);


			Data.Add((byte)value);
			Data.Add((byte)(value >> 8));
			Data.Add((byte)(value >> 0x10));
			Data.Add((byte)(value >> 0x18));

			//index += 4;
		}
		public void WriteInt32(long value)
		{
			//if (index > Length - 4)
			//{
			//    throw new IndexOutOfRangeException("WriteInt32");
			//}

			//byte[] tmp = BitConverter.GetBytes(value);
			//Data.AddRange(tmp);


			Data.Add((byte)value);
			Data.Add((byte)(value >> 8));
			Data.Add((byte)(value >> 0x10));
			Data.Add((byte)(value >> 0x18));

			//index += 4;
		}

		public void WriteUInt32(uint value)
		{
			WriteInt32((int)value);
		}

		public void WriteInt64(long value)
		{
			byte[] tmp = BitConverter.GetBytes(value);
			Data.AddRange(tmp);
		}

		public void WriteUInt64(ulong value)
		{
			byte[] tmp = BitConverter.GetBytes(value);
			Data.AddRange(tmp);
		}

		public void WriteByte(byte value)
		{
			//if (index > Length - 1)
			//{
			//    throw new IndexOutOfRangeException("WriteByte");
			//}

			Data.Add(value);
			//index += 1;
		}

		public void WriteName(string name, int maxLength = 15)
		{
			var bMarriedName = Encoding.GetEncoding(1258).GetBytes(name);
			if (bMarriedName.Length > maxLength)
				WriteByte(bMarriedName, 0, maxLength);
			else
			{
				WriteByte(bMarriedName, bMarriedName.Length);
				WriteByte(new byte[maxLength - bMarriedName.Length], maxLength - bMarriedName.Length);
			}
		}

		public void WriteByte(byte[] value, int length)
		{
			//if (index > Length - length)
			//{
			//    throw new IndexOutOfRangeException("WriteByte with Length");
			//}

			for (int i = 0; i < length; i++)
			{
				WriteByte(value[i]);
			}
		}

		public void WriteByte(byte[] value, int start, int length)
		{
			//if (index > Length - length)
			//{
			//    throw new IndexOutOfRangeException("WriteByte with Length");
			//}

			for (int i = start; i < length + start; i++)
			{
				WriteByte(value[i]);
			}
		}

		public void WriteString(string text)
		{
			//if (index > Length - text.Length)
			//{
			//    throw new IndexOutOfRangeException("WriteString");
			//}

			byte[] data = Encoding.GetEncoding(1258).GetBytes(text); //each char becomes 2 bytes
			WriteByte(data, data.Length);
		}

		public void WriteString(string text, int skip)
		{
			//if (index > Length - (text.Length + skip))
			//{
			//    throw new IndexOutOfRangeException("WriteString");
			//}

			byte[] data = Encoding.GetEncoding(1258).GetBytes(text); //each char becomes 2 bytes
			WriteByte(data, data.Length);

			WriteByte(new byte[skip], skip);
			//index += skip - text.Length;
		}

		public void WriteSingle(float value)
		{
			//if (index > Length - 4)
			//{
			//    throw new IndexOutOfRangeException("WriteSingle");
			//}


			byte[] tmp = BitConverter.GetBytes(value);
			Data.AddRange(tmp);

			//Data[index] = tmp[0];
			//Data[index + 1] = tmp[1];
			//Data[index + 2] = tmp[2];
			//Data[index + 3] = tmp[3];

			//index += 4;
		}

		public void WriteDouple(double value)
		{
			//if (index > Length - 4)
			//{
			//    throw new IndexOutOfRangeException("WriteSingle");
			//}


			byte[] tmp = BitConverter.GetBytes(value);
			Data.AddRange(tmp);

			//Data[index] = tmp[0];
			//Data[index + 1] = tmp[1];
			//Data[index + 2] = tmp[2];
			//Data[index + 3] = tmp[3];

			//index += 4;
		}
		#endregion

		#region Setting



		#endregion

		public void SkipIndex(int skip)
		{
			index += skip;
		}
		public void SetIndex(int ind)
		{
			index = ind;
		}

		public byte[] GetByteArray()
		{
			return Data.ToArray();
		}

		#region Dispose

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private bool _isDisposed = false;

		private void Dispose(bool disposing)
		{
			if (_isDisposed)
				return;

			// Free any unmanaged objects here.
			_isDisposed = true;

			if (disposing)
			{
				Data.Clear();
				Data = null;
			}
		}

		#endregion
	}
}
