namespace MultiplayerEvents{
    public static class Chat{
        public static void AddMessage(string message){
             if (HkmpPouch.Client.Instance.clientApi != null){
                HkmpPouch.Client.Instance.clientApi.UiManager.ChatBox.AddMessage(message);
             }
        }
    }
}