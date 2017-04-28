using System;
using System.Diagnostics.CodeAnalysis;

namespace JPRE.xx.Packet
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public abstract class AbstractPacket : BinaryStream
    {
        public AbstractPacket(BinaryStream unpack) : base(unpack)
        {
        }

        public AbstractPacket() : this(new byte[] { })
        {
        }

        public AbstractPacket(byte[] data) : base(data)
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

        public const byte NetworkId = 0;

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
            Packets = new Type[32];
            PacketIds = new byte[32];
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

        private static Type[] Packets;

        private static byte[] PacketIds;

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

                    var values = typeof(PacketId).GetFields();
                    foreach (var value in values)
                    {
                        if (!value.Name.Equals(name)) continue;
                        PacketIds[_packetsCount] = (byte) value.GetValue(null);
                        break;
                    }
                }

                if (PacketIds[_packetsCount] == 0)
                {
                    PacketIds[_packetsCount] = (byte) type.GetField("NETWORK_ID").GetValue(null);
                }
                Packets[_packetsCount++] = type;
            }
            catch (Exception e)
            {
                MApi.Api_OutPut(e.ToString());
                throw;
            }
        }

        public static Type[] GetPackets()
        {
            return Packets;
        }

        public static byte[] GetPacketIds()
        {
            return PacketIds;
        }

        public static int GetPacketsCount()
        {
            return _packetsCount;
        }

        public static AbstractPacket MatchPacket(byte id)
        {
            for (var i = 0; i < PacketIds.Length; i++)
            {
                if (PacketIds[i] != id) continue;
                var constructor = Packets[i].GetConstructor(new Type[0]);
                if (constructor != null) return (AbstractPacket) constructor.Invoke(new object[0]);
            }
            return null;
        }

        public override string ToString()
        {
            return GetType().Name + "(Id:" + GetNetworkId() + ")";
        }
    }
}