namespace JPRE.xx.Packet
{
    public static class Protocol
    {
        public static readonly byte[] Signature = {127, 127, 127, 127};

        public const byte ClientReload = 1;
        public const byte ClientPing = 2;
        public const byte ClientEvent = 3;
        public const byte ClientGetPluginList = 4;
        public const byte ClientGetPluginInformation = 5;
        public const byte ClientCommandResult = 6;
        public const byte ClientStaticCommandResult = 6;

        public const byte ServerPong = 7;
        public const byte ServerCommand = 8;
        public const byte ServerStaticCommand = 14;
        public const byte ServerInvalidEvent = 9;
        public const byte ServerEventResult = 10;
        public const byte ServerInvalidId = 11;
        public const byte ServerLog = 12;
        public const byte ServerGetPluginInformationResult = 13;
    }
}