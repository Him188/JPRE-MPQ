using System;

namespace JPRE.xx
{
    public static class Binary {
        public static byte[] ToBytes(int value) {
            var data = new byte[4];
            data[0] = (byte) (value >> 24);
            data[1] = (byte) (value >> 16);
            data[2] = (byte) (value >> 8);
            data[3] = (byte) (value);
            return (data);
        }

        public static byte[] ToBytes(bool value) {
            return (new[]{(byte) (value ? 1 : 0)});
        }

        public static byte[] ToBytes(float value) {
            return ToBytes(BitConverter.DoubleToInt64Bits(value));
        }

        public static byte[] ToBytes(long number) {
            var temp = number;
            var b = new byte[8];
            for (var i = 0; i < b.Length; i++) {
                b[i] = (byte)(temp & 0xff);
                temp = temp >> 8;
            }
            return b;
        }

        public static byte[] ToBytes(short value) {
            var data = new byte[2];
            data[2] = (byte) (value >> 8);
            data[3] = (byte) (value);
            return (data);
        }

        public static byte[] ToBytes(double value) {
            return ToBytes(BitConverter.DoubleToInt64Bits(value));
        }

        public static double ToDouble(byte[] bytes) {
            return BitConverter.Int64BitsToDouble((bytes[0] & 0xff |
                                            (long) (bytes[1] & 0xff) << 8) |
                                           ((long) (bytes[2] & 0xff) << 16) |
                                           ((long) (bytes[3] & 0xff) << 24) |
                                           ((long) (bytes[4] & 0xff) << 32) |
                                           ((long) (bytes[5] & 0xff) << 40) |
                                           ((long) (bytes[6] & 0xff) << 48) |
                                           ((long) (bytes[7] & 0xff) << 56));
        }

        public static float ToFloat(byte[] bytes) {
            return (float) BitConverter.Int64BitsToDouble((bytes[0] << 24) + (bytes[1] << 16) + (bytes[2] << 8) + bytes[3]);
        }

        public static int ToInt(byte[] bytes) {
            return (bytes[0] << 24) + (bytes[1] << 16) + (bytes[2] << 8) + bytes[3];
        }

        public static long ToLong(byte[] bytes) {
            return (bytes[0] & 0xff |
                    (long) (bytes[1] & 0xff) << 8) |
                   ((long) (bytes[2] & 0xff) << 16) |
                   ((long) (bytes[3] & 0xff) << 24) |
                   ((long) (bytes[4] & 0xff) << 32) |
                   ((long) (bytes[5] & 0xff) << 40) |
                   ((long) (bytes[6] & 0xff) << 48) |
                   ((long) (bytes[7] & 0xff) << 56);
        }

        public static short ToShort(byte[] bytes) {
            return (short) ((bytes[0] << 8) + bytes[1]);
        }

        public static bool ToBoolean(byte[] bytes) {
            return bytes[0] == 1;
        }

        public static byte[] RealReverse(byte[] array) {
            var newArray = new byte[array.Length];
            for (var i = 0; i < array.Length; i++) {
                newArray[i] = array[array.Length - i - 1];
            }
            return newArray;
        }
    }

}