namespace JPRE.xx.Packet
{
    public class ClientPingPacket : AbstractPacket
    {
        public override void Encode()
        {
            SetEncoded(true);
            Clear();

        }

        public override void Decode()
        {
        }

        public override byte GetNetworkId()
        {
            return PacketId.ClientPing;
        }
    }
}