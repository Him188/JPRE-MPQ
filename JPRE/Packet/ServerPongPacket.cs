namespace JPRE.xx.Packet
{
    public class ServerPongPacket : AbstractPacket
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
            return Protocol.ServerPong;
        }
    }
}