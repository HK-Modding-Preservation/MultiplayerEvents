namespace MultiplayerEvents
{
    public static class Chat{
        public static void AddMessage(string message){
             if (MultiplayerEvents.Instance.pipe != null && MultiplayerEvents.Instance.pipe.ClientApi != null){
                MultiplayerEvents.Instance.pipe.ClientApi.UiManager.ChatBox.AddMessage(message);
             }
        }
    }
}