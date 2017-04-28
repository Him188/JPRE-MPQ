namespace JPRE.xx.Packet
{
    public sealed class ClientEventPacket : AbstractPacket
    {
        private readonly int _id;
        private readonly object[] _args;

        public ClientEventPacket(int id,params object[] args)
        {
            _id = id;
            _args = args;
        }

        public override void Encode()
        {
            SetEncoded(true);
            Clear();

            PutInt(_id);
            PutRawWithType(_args);
        }

        public override void Decode()
        {
        }

        public override byte GetNetworkId()
        {
            return Protocol.ClientEvent;
        }
    }
}