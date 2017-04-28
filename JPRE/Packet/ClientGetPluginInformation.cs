namespace JPRE.xx.Packet
{
    public class ClientGetPluginInformation : AbstractPacket
    {
        private readonly string _plugin;

        public ClientGetPluginInformation(string plugin)
        {
            _plugin = plugin;
        }

        public override void Encode()
        {
            SetEncoded(true);
            Clear();

            PutString(_plugin);
        }

        public override void Decode()
        {
        }

        public override byte GetNetworkId()
        {
            return PacketId.ClientGetPluginInformation;
        }
    }
}