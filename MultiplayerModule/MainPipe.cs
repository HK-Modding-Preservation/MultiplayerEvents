namespace MultiplayerEvents.MultiplayerModule
{
    internal class MainPipe
    {
        internal static MainPipe Instance = new MainPipe();

        internal PipeClient pipe;

        public MainPipe()
        {
            Instance = this;
            pipe = new PipeClient("MultiplayerEvents");
        }
        internal bool isReady()
        {
            return (pipe?.ClientApi?.NetClient != null && pipe.ClientApi.NetClient.IsConnected);
        }

    }
}
