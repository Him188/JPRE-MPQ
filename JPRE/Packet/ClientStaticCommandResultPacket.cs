namespace JPRE.xx.Packet
{
    public class ClientStaticCommandResultPacket : AbstractPacket
    {
        public readonly object _result;

        public ClientStaticCommandResultPacket(object result)
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
            return Protocol.ClientStaticCommandResult;
        }
    }
}