namespace JPRE.xx.Packet
{
    public class ServerLogPacket : AbstractPacket
    {
        public string Log { get; private set; }

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

            Log = GetString();
        }

        public override byte GetNetworkId()
        {
            return Protocol.ServerLog;
        }
    }
}