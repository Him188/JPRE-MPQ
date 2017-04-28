namespace JPRE.xx.Packet
{
    public class ServerInvalidIdPacket : AbstractPacket
    {
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

        }

        public override byte GetNetworkId()
        {
            return Protocol.ServerInvalidId;
        }
    }
}