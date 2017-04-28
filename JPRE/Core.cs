using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JPRE.xx.Packet;
using RGiesecke.DllExport;

namespace JPRE.xx
{
    public class Core
    {
        public static Queue<object> Results = new Queue<object>();

        private static Network _network;


        [DllExport("Message", CallingConvention = CallingConvention.Cdecl)]
        public static int Message(string robotQq, int msgType, string msgRaw, string cookies, string sessionKey,
            string clinetKey)
        {
            return 1;
        }

        [DllExport("info", CallingConvention = CallingConvention.Cdecl)]
        public static string Info()
        {
            return "JPRE Plugin";
        }


        private static readonly byte[] EventLock = { };
        public static bool Started = false;

        [DllExport("EventFun", CallingConvention = CallingConvention.Cdecl)]
        public static int EventFun(string robotQq, int msgType, int msgSubType, string msgSrc, string targetActive,
            string targetPassive, string msgContent, string msgRaw, IntPtr mPointer)
        {
            try
            {
                if (msgType == 10000)
                {
                    try
                    {
                        AbstractPacket.InitPacket();
                        _network = new Network(420, "127.0.0.1");
                    }
                    catch (Exception e)
                    {
                        MApi.Api_OutPut(e.ToString());
                    }
                }

                if (!Started || _network == null || !_network.IsConnected() || msgType == -1) //-1: 未定义
                {
                    return 1;
                }


                if (targetActive.Length == 0)
                {
                    targetActive = "0";
                }

                if (targetPassive.Length == 0)
                {
                    targetPassive = "0";
                }

                if (msgSrc.Length == 0)
                {
                    msgSrc = "0";
                }

                lock (EventLock)
                {
                    Console.WriteLine("hey");
                    _network.SendPacket(new ClientEventPacket(msgType, long.Parse(robotQq), msgSubType,
                        long.Parse(msgSrc),
                        long.Parse(targetActive),
                        long.Parse(targetPassive), msgContent, msgRaw));

                    while (Results.Count == 0)
                    {
                    }

                    object result;
                    try
                    {
                        result = Results.Dequeue();
                    }
                    catch (InvalidOperationException)
                    {
                        return 0;
                    }
                    if (result == null)
                    {
                        return 1;
                    }
                    result = result.ToString().ToLower();
                    if (result.Equals("false"))
                    {
                        return 0;
                    }

                    if (result.Equals("true"))
                    {
                        return 1;
                    }

                    if (result.Equals(""))
                    {
                        return 0;
                    }
                    return int.Parse(result.ToString());
                }
            }
            catch (Exception e)
            {
                MApi.Api_OutPut(e.ToString());
            }
            return 1;
        }


        [DllExport("about", CallingConvention = CallingConvention.Cdecl)]
        public static void About()
        {
            //TODO
        }

        [DllExport("end", CallingConvention = CallingConvention.Cdecl)]
        public static int End()
        {
            return 0;
        }
    }
}