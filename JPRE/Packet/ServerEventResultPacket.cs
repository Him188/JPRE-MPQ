namespace JPRE.xx.Packet
{
    public sealed class ServerEventResultPacket : AbstractPacket
    {
        public readonly object _result;

        public ServerEventResultPacket(object result)
        {
            _result = result;
        }

        public override void Encode()
        {
        }

        public override void Decode()
        {
            if (!IsEncoded())
            {
                return;
            }
            SetEncoded(false);

            PutRaw(_result);
        }

        public override byte GetNetworkId()
        {
            return PacketId.ServerEventResult;
        }
    }
}