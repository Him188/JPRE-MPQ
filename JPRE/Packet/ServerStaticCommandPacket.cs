namespace JPRE.xx.Packet
{
    public class ServerStaticCommandPacket : AbstractPacket
    {
        public CommandId Id { get; private set; }
        public object[] Args { get; private set; }

        public ServerStaticCommandPacket(object[] args, CommandId id)
        {
            Args = args;
            Id = id;
        }


        public override void Encode()
        {
            throw new System.NotImplementedException();
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
            return PacketId.ServerStaticCommand;
        }
    }
}