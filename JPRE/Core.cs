using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using JPRE.xx.Packet;
using RGiesecke.DllExport;

namespace JPRE.xx
{
    public class Core
    {
        public static Queue<object> Results = new Queue<object>();

        private static Network _network = null;


        [DllExport("Message", CallingConvention = CallingConvention.Cdecl)]
        public static int Message(string robotQq, int msgType, string msgRaw, string cookies, string sessionKey,
            string clinetKey)
        {
            return 1;
        }

        [DllExport("info", CallingConvention = CallingConvention.Cdecl)]
        public static string Info()
        {
            AbstractPacket.InitPacket();
            MApi.Api_OutPut("JPRE 正在初始化");
            MApi.Api_OutPut("JPRE 正在启动TCP客户端并连接 Java...");
            return "JPRE Plugin";
        }


        private static readonly byte[] EventLock = { };
        public static bool Started = false;

        [DllExport("EventFun", CallingConvention = CallingConvention.Cdecl)]
        public static int EventFun(string robotQq, int msgType, int msgSubType, string msgSrc, string targetActive,
            string targetPassive, string msgContent, string msgRaw, IntPtr mPointer)
        {
            MApi.Api_OutPut("检测到事件, ID:" + msgType);

            //return 1;

            if (msgType == 12000)
            {
                return 1;
            }

            if (!Started)
            {
                return 1;
            }

            // return 1;

            if (_network == null)
            {
                return 1;
            }

            if (!_network.IsConnected())
            {
                return 1;
            }
            MApi.Api_OutPut("网络正常, 正在发送包");

            //lock (EventLock)
            //{

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


            _network.SendPacket(new ClientEventPacket(msgType, long.Parse(robotQq), msgSubType, long.Parse(msgSrc),
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
            return int.Parse(result.ToString());
            //}
        }


        [DllExport("about", CallingConvention = CallingConvention.Cdecl)]
        public static void About()
        {
            _network = new Network(420, "127.0.0.1");

        }

        [DllExport("end", CallingConvention = CallingConvention.Cdecl)]
        public static int End()
        {
            return 0;
        }
    }
}