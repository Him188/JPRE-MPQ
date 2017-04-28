using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using JPRE.xx.Packet;

namespace JPRE.xx
{
    public class Network
    {
        private readonly Socket _client;

        private bool _running;

        public bool IsConnected()
        {
            return _client != null && _client.Connected;
        }

        public void Shutdown()
        {
            _running = false;
            _client.Shutdown(SocketShutdown.Both);
        }

        public Network(int port, string host)
        {
            try
            {
                var ip = IPAddress.Parse(host);

                var ipe = new IPEndPoint(ip, port);

                _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                _client.Connect(ipe);

                _running = true;

                //receive

                var thread = new Thread(() =>
                {
                    while (_running)
                    {
                        try
                        {
                            var recBytes = new byte[128];
                            var count = _client.Receive(recBytes, 0);

                            var bytes = new byte[count];
                            Array.Copy(recBytes, 0, bytes, 0, count);

                            HandlePacket(recBytes);
                        }
                        catch (Exception e)
                        {
                            MApi.Api_OutPut(e.ToString());
                            MApi.Api_OutPut("接受数据线程发生了异常, 一上为错误信息");
                            throw;
                        }
                    }
                });
                thread.Start();
            }
            catch (Exception e)
            {
                MApi.Api_OutPut(e.ToString());
                MApi.Api_OutPut("无法启动TPC客户端, 以上为错误信息");
            }

            Core.Started = true;
        }


        private byte[] _temp = new byte[0];

        private static readonly byte[] Signature = {127, 127, 127, 127};

        /// <summary>
        /// 接受到客户端数据后的分包/合包/处理过程
        ///
        /// 该方法单线程运行, 不需要锁
        /// </summary>
        /// <param name="data">接收到的数据</param>
        /// <returns></returns>
        private void HandlePacket(byte[] data)
        {
            MApi.Api_OutPut("收到数据包: " + Utils.ArrayToString(data));
            MApi.Api_OutPut("开始分包/粘包");
            try
            {
                _temp = Utils.ConcatArray(_temp, data);
                while (_temp.Length != 0)
                {
                    var position = Utils.ArraySearch(_temp, Signature);
                    if (position == -1)
                    {
                        return; //收到的是子包, 数据未结尾
                    }

                    var d = Utils.ArrayGetCenter(_temp, 0, position);
                    _temp = Utils.ArrayDelete(_temp, position);
                    ProcessPacket(d);
                }
            }
            catch (Exception e)
            {
                MApi.Api_OutPut(e.ToString());
                throw;
            }
        }

        private void ProcessPacket(byte[] data)
        {
            ProcessPacket(new BinaryStream(data));
        }

        private void ProcessPacket(BinaryStream stream)
        {
            try
            {
                MApi.Api_OutPut("[Network] Data packet received: " + Utils.ArrayToString(stream.GetAll()));

                var pk = AbstractPacket.MatchPacket(stream.GetByte());
                if (pk == null)
                {
                    return;
                }
                pk.Decode();

                switch (pk.GetNetworkId())
                {
                    case PacketId.ServerCommand:
                        var commandPacket = (ServerCommandPacket) pk;

                        foreach (var methodInfo in typeof(MApi).GetMethods())
                        {
                            if (!methodInfo.Name.ToLower().EndsWith(commandPacket.Id.ToString().ToLower())) continue;
                            pk = new ClientCommandResultPacket(methodInfo.Invoke(null, commandPacket.Args));
                            SendPacket(pk);
                            return;
                        }

                        SendPacket(new ClientCommandResultPacket(""));
                        return;

                    case PacketId.ServerInvalidId: //packet id
                        return;

                    case PacketId.ServerGetPluginInformationResult:
                        //TODO
                        return;

                    case PacketId.ServerLog:
                        var logPacket = (ServerLogPacket) pk;

                        var strings = logPacket.Log.Split("||".ToCharArray(), 3);

                        MApi.Api_OutPut("[" + strings[1] + "] " + strings[2]);
                        return;

                    case PacketId.ServerPong:
                        //TODO
                        return;

                    case PacketId.ServerStaticCommand:
                        var sCommandPacket = (ServerStaticCommandPacket) pk;

                        var args = new object[sCommandPacket.Args.Length - 1];
                        Array.Copy(sCommandPacket.Args, 1, args, 0, sCommandPacket.Args.Length - 1);

                        foreach (var methodInfo in typeof(MApi).GetMethods())
                        {
                            if (!methodInfo.Name.ToLower().EndsWith(sCommandPacket.Id.ToString().ToLower())) continue;
                            pk = new ClientStaticCommandResultPacket(methodInfo.Invoke(null, args));
                            SendPacket(pk);
                            return;
                        }

                        SendPacket(new ClientStaticCommandResultPacket(""));
                        return;

                    case PacketId.ServerEventResult:
                        var serverEventResultPacket = pk as ServerEventResultPacket;
                        if (serverEventResultPacket != null)
                            Core.Results.Enqueue(serverEventResultPacket._result);
                        return;

                    case PacketId.ServerInvalidEvent:
                        Core.Results.Enqueue("");

                        return;

                    default:
                        return;
                }
            }
            catch (Exception e)
            {
                MApi.Api_OutPut(e.ToString());
                MApi.Api_OutPut("在处理接收到的数据包时出现异常, 以上为错误信息");
            }
        }

        ~Network()
        {
            Shutdown();
        }

        public void SendPacket(AbstractPacket packet)
        {
            try
            {
                packet.Encode();
                var data = packet.GetAll();
                var result = new byte[data.Length + 1]; //数据包ID
                result[0] = packet.GetNetworkId();
                Array.Copy(data, 0, result, 1, data.Length);
                _client.Send(result);
                MApi.Api_OutPut("[Network] 数据包已发送:" + packet + ", 数据为: " + Utils.ArrayToString(result));
            }
            catch (Exception e)
            {
                MApi.Api_OutPut(e.ToString());
                MApi.Api_OutPut("在发送数据包时出现了异常, 以上为错误信息");
            }
        }
    }
}