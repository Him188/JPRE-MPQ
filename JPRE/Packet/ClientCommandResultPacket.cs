namespace JPRE.xx.Packet
{
    public class ClientCommandResultPacket : AbstractPacket
    {
        private readonly object _result;

        public ClientCommandResultPacket(object result)
        {
            _result = result;
        }

        public override void Encode()
        {
            SetEncoded(true);
            Clear();

            PutRaw(_result);
        }

        public override void Decode()
        {
        }

        public override byte GetNetworkId()
        {
            return PacketId.ClientCommandResult;
        }
    }
}