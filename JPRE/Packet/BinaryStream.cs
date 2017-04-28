using System;
using System.Collections.Generic;
using System.Globalization;

namespace JPRE.xx.Packet
{
    public class BinaryStream
    {
        private byte[] _data;
        private int _location;

        public BinaryStream(BinaryStream unpack) : this(unpack.GetAll())
        {
        }

        public BinaryStream() : this(new byte[] { })
        {
        }

        public BinaryStream(byte[] data)
        {
            SetData(data);
        }


        public void SetData(byte[] data)
        {
            _data = data;
        }

        public byte[] GetAll()
        {
            return _data;
        }

        public void Clear()
        {
            _data = new byte[0];
        }


        /* Putter */

        public void PutInt(int value)
        {
            PutBytes(Binary.ToBytes(value));
        }

        public void PutLong(long value)
        {
            PutBytes(Binary.ToBytes(value));
        }

        public void PutShort(short value)
        {
            PutBytes(Binary.ToBytes(value));
        }

        public void PutBool(bool value)
        {
            PutBytes(Binary.ToBytes(value));
        }

        public void PutFloat(float value)
        {
            PutBytes(Binary.ToBytes(value));
        }

        public void PutDouble(double value)
        {
            PutBytes(Binary.ToBytes(value));
        }

        public void PutByte(byte value)
        {
            if (_data.Length <= _location + 1)
            {
                var newArray = new byte[_data.Length + 1];
                Array.Copy(_data, 0, newArray, 0, _data.Length);
                _data = newArray;
            }
            _data[_location++] = value;
        }

        public void PutString(string value)
        {
            if (value == null)
            {
                return;
            }

            try
            {
                PutInt(System.Text.Encoding.Default.GetBytes(value).Length);
                PutBytes(System.Text.Encoding.Default.GetBytes(value));
            }
            catch (Exception e)
            {
                MApi.Api_OutPut(e.ToString());
            }
        }

        public void PutBytes(IEnumerable<byte> value)
        {
            foreach (var aValue in value)
            {
                PutByte(aValue);
            }
        }

        public void PutRaw(params object[] values)
        {
            PutRawWithType();
            /*
            foreach (var value in values)
            {
                if (value is int)
                {
                    PutInt((int) value);
                }
                else if (value is byte)
                {
                    PutByte((byte) value);
                }
                else if (value is long)
                {
                    PutLong((long) value);
                }
                else if (value is string)
                {
                    PutString((string) value);
                }
                else if (value is bool)
                {
                    PutBool((bool) value);
                }
                else if (value is double)
                {
                    PutDouble((double) value);
                }
                else if (value is short)
                {
                    PutShort((short) value);
                }
                else if (value is float)
                {
                    PutFloat((float) value);
                }
                else if (value is byte[])
                {
                    PutBytes((byte[]) value);
                }
                else
                {
                    MApi.Api_OutPut("[AbstractPacket] putRaw: wrong type of values");
                }
            }
            */
        }

        public void PutRawWithType(params object[] values)
        {
            foreach (var value in values)
            {
                if (value is int)
                {
                    PutByte(0);
                    PutInt((int) value);
                }
                else if (value is byte)
                {
                    PutByte(1);
                    PutByte((byte) value);
                }
                else if (value is long)
                {
                    PutByte(2);
                    PutLong((long) value);
                }
                else if (value is string)
                {
                    PutByte(3);
                    PutString((string) value);
                }
                else if (value is bool)
                {
                    PutByte(4);
                    PutBool((bool) value);
                }
                else if (value is double)
                {
                    PutByte(5);
                    PutDouble((double) value);
                }
                else if (value is short)
                {
                    PutByte(6);
                    PutShort((short) value);
                }
                else if (value is float)
                {
                    PutByte(7);
                    PutFloat((float) value);
                }
                else if (value is byte[])
                {
                    PutByte(8);
                    PutInt(((byte[]) value).Length);
                    PutBytes((byte[]) value);
                }
                else
                {
                    MApi.Api_OutPut("[AbstractPacket] putRaw: wrong type of values");
                }
            }
        }

        public void PutList<T>(List<T> list, Type valueType)
        {
            PutString(valueType.Name);
            PutInt(list.Count);
            list.ForEach(o => PutRawWithType(o));
        }


        /* Getter */
        public byte[] GetCenter(int location, int length)
        {
            if (length == 0)
            {
                return new byte[0];
            }
            var result = new byte[length];
            try
            {
                Array.Copy(_data, location, result, 0, length);
            }
            catch (IndexOutOfRangeException e)
            {
                MApi.Api_OutPut(e.ToString());
            }

            return result;
        }

        public int GetLastLength()
        {
            return _data.Length - _location;
        }


        public byte[] GetBytes(int length)
        {
            byte[] result = GetCenter(_location, length);

            _location += length;
            return result;
        }

        public byte[] GetLast()
        {
            return GetBytes(GetLastLength());
        }

        public byte GetByte()
        {
            return GetBytes(1)[0];
        }

        public int GetInt()
        {
            return Binary.ToInt(GetBytes(4));
        }

        public long GetLong()
        {
            return Binary.ToLong(GetBytes(8));
        }

        public short GetShort()
        {
            return Binary.ToShort(GetBytes(2));
        }

        public string GetString()
        {
            return System.Text.Encoding.Default.GetString(GetBytes(GetInt()));
        }

        public bool GetBool()
        {
            return Binary.ToBoolean(GetBytes(1));
        }

        public double GetDouble()
        {
            return Binary.ToDouble(GetBytes(8));
        }

        public float GetFloat()
        {
            return Binary.ToFloat(GetBytes(4));
        }

        public object GetRaw()
        {
            switch (GetByte())
            {
                case 0:
                    return GetInt();
                case 1:
                    return GetByte();
                case 2:
                    return GetLong();
                case 3:
                    return GetString();
                case 4:
                    return GetBool();
                case 5:
                    return GetDouble().ToString(CultureInfo.InvariantCulture);
                case 6:
                    return GetShort();
                case 7:
                    return GetFloat().ToString(CultureInfo.InvariantCulture);
                case 8:
                case 9:
                    return System.Text.Encoding.Default.GetString(GetBytes(GetInt()));
                default:
                    throw new ArgumentException("wrong type of values");
            }
        }

        public List<object> GetList()
        {
            Type type;
            try
            {
                type = Type.GetType(GetString());
            }
            catch (TypeLoadException e)
            {
                MApi.Api_OutPut(e.ToString());
                return null;
            }

            var result = new List<object>();
            for (var i = 0; i < GetInt(); i++)
            {
                if (type != null) result.Add(Convert.ChangeType(GetRaw(), type));
            }
            return result;
        }

        public List<T> GetList<T>()
        {
            Type type;
            try
            {
                type = Type.GetType(GetString());
            }
            catch (TypeLoadException e)
            {
                MApi.Api_OutPut(e.ToString());
                return null;
            }

            var result = new List<T>();
            for (var i = 0; i < GetInt(); i++)
            {
                if (type != null) result.Add((T) Convert.ChangeType(GetRaw(), type));
            }
            return result;
        }
    }
}