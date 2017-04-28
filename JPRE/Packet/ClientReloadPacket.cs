namespace JPRE.xx.Packet
{
    public class ClientReloadPacket : AbstractPacket
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
            return PacketId.ClientReload;
        }
    }
}