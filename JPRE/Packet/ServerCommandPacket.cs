namespace JPRE.xx.Packet
{
    public class ServerCommandPacket : AbstractPacket
    {
        public CommandId Id { get; private set; }
        public object[] Args { get; private set; }

        public ServerCommandPacket(object[] args, CommandId id)
        {
            Args = args;
            Id = id;
        }


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

            Id = (CommandId) GetInt();
            Args = GetList().ToArray();
        }

        public override byte GetNetworkId()
        {
            return Protocol.ServerCommand;
        }
    }
}