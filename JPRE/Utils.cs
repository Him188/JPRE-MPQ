using System;

namespace JPRE.xx
{
    public static class Utils
    {
        public static string ArrayToString<T>(T[] array)
        {
            var result = array.GetType().Name + "(" + array.Length + "){";
            foreach (var c in array)
            {
                result += c + ",";
            }
            result = result.Substring(0, result.Length - 1);
            result += "}";
            return result;
        }

        public static int ArraySearch(byte[] array, byte[] search)
        {
            if (search.Length == 0)
            {
                return -1;
            }

            int found;
            loop:
            while (true)
            {
                found = Array.BinarySearch(array, search[0]);
                for (var i = 1; i < search.Length; i++)
                {
                    try
                    {
                        if (array[found + i] != search[i])
                        {
                            goto loop;
                        }
                    }
                    catch (Exception e)
                    {
                        return -1;
                    }
                }
                break;
            }

            return found;
        }

        public static byte[] ArrayGetCenter(byte[] array, int location, int length)
        {
            if (length == 0)
            {
                return new byte[0];
            }
            var result = new byte[length];
            Array.Copy(array, location, result, 0, length);
            return result;
        }

        public static T[] ConcatArray<T>(T[] original, T[] target)
        {
            var newArray = new T[original.Length + target.Length];
            Array.Copy(original, 0, newArray, 0, original.Length);
            Array.Copy(target, 0, newArray, original.Length, target.Length);
            return newArray;
        }

        public static byte[] ArrayDelete(byte[] array, int length)
        {
            var newArray = new byte[array.Length - length];
            Array.Copy(array, 0, newArray, 0, array.Length - length);
            return newArray;
        }
    }
}