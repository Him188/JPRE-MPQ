using System;
using System.Diagnostics.CodeAnalysis;

namespace JPRE.xx.Packet
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public abstract class AbstractPacket : BinaryStream
    {
        protected AbstractPacket(BinaryStream unpack) : base(unpack)
        {
        }

        protected AbstractPacket() : this(new byte[] { })
        {
        }

        protected AbstractPacket(byte[] data) : base(data)
        {
        }

        private bool _encoded;

        public bool IsEncoded()
        {
            return _encoded;
        }

        public bool SetEncoded(bool encoded)
        {
            var original = _encoded;
            _encoded = encoded;
            return original;
        }


        public abstract void Encode();

        public abstract void Decode();

        public abstract byte GetNetworkId();

        public static byte GetNetworkId(AbstractPacket packet)
        {
            return GetNetworkId(packet.GetType());
        }

        public static byte GetNetworkId(Type packet)
        {
            var field = packet.GetField("NETWORK_ID");
            //field.setAccessible(true); //throw an IllegalAccessException instead when the field is not accessible
            return (byte) field.GetValue(null);
        }

        public static void InitPacket()
        {
            _packets = new Type[32];
            _packetIds = new byte[32];
            _packetsCount = 0;

            try
            {
                foreach (var aType in new[]
                {
                    typeof(ServerCommandPacket),
                    typeof(ServerEventResultPacket),
                    typeof(ServerGetPluginInformationResultPacket),
                    typeof(ServerInvalidEventPacket),
                    typeof(ServerInvalidIdPacket),
                    typeof(ServerLogPacket),
                    typeof(ServerPongPacket),
                    typeof(ServerStaticCommandPacket),
                })
                {
                    //noinspection unchecked
                    RegisterPacket(aType);
                }
            }
            catch (Exception e)
            {
                MApi.Api_OutPut(e.ToString());
            }
        }

        private static Type[] _packets;

        private static byte[] _packetIds;

        private static int _packetsCount;

        public static void RegisterPacket(Type type)
        {
            try
            {
                if (type.IsAbstract)
                {
                    throw new ArgumentException("cannot register a abstract class");
                }


                if (type.Name.EndsWith("Packet"))
                {
                    var name = type.Name.Substring(0, type.Name.Length - "Packet".Length);

                    var values = typeof(Protocol).GetFields();
                    foreach (var value in values)
                    {
                        if (!value.Name.Equals(name)) continue;
                        _packetIds[_packetsCount] = (byte) value.GetValue(null);
                        break;
                    }
                }

                if (_packetIds[_packetsCount] == 0)
                {
                    _packetIds[_packetsCount] = (byte) type.GetField("NETWORK_ID").GetValue(null);
                }

                _packets[_packetsCount++] = type;
            }
            catch (Exception e)
            {
                MApi.Api_OutPut(e.ToString());
            }
        }

        public static Type[] GetPackets()
        {
            return _packets;
        }

        public static byte[] GetPacketIds()
        {
            return _packetIds;
        }

        public static int GetPacketsCount()
        {
            return _packetsCount;
        }

        public static AbstractPacket MatchPacket(byte id)
        {
            for (var i = 0; i < _packetIds.Length; i++)
            {
                if (_packetIds[i] != id) continue;
                return (AbstractPacket) Activator.CreateInstance(_packets[i]);
            }
            return null;
        }

        public override string ToString()
        {
            return GetType().Name + "(Id:" + GetNetworkId() + ")";
        }
    }
}