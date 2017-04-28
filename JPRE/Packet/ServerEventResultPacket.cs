namespace JPRE.xx.Packet
{
    public sealed class ServerEventResultPacket : AbstractPacket
    {
        public object Result;

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

            Result = GetRaw();
        }

        public override byte GetNetworkId()
        {
            return Protocol.ServerEventResult;
        }
    }
}