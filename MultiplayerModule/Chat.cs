namespace MultiplayerEvents.MultiplayerModule
{
    public static class Chat
    {
        public static void AddMessage(string message)
        {
            if (MainPipe.Instance.isReady())
            {
                MainPipe.Instance.pipe.ClientApi.UiManager.ChatBox.AddMessage(message);
            }
        }
    }
}