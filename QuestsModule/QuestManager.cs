using MultiplayerEvents.MultiplayerModule;

namespace MultiplayerEvents.QuestsModule
{
    public class QuestManager
    {
        public List<CounterQuest> CounterQuests = new();
        public Dictionary<string, List<string>> EnemyNames;
        public Dictionary<string, string> internalToExternalNames = new();
        public System.Random rnd = new System.Random();
        private PipeClient pipe { get => MainPipe.Instance.pipe; }
        public void Init()
        {
            pipe.OnReady += Pipe_OnReady;
        }
        public void loadEnemyMaps()
        {
            if (EnemyNames != null) { return; }
            var enemyNames = System.Text.Encoding.Default.GetString(AssemblyUtils.GetBytesFromResources("EnemyNameMap.json"));
            EnemyNames = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(enemyNames);
            foreach (var kvp in EnemyNames)
            {
                foreach (var v in kvp.Value)
                {
                    internalToExternalNames[v] = kvp.Key;
                }
            }
        }
        public void createNewQuests()
        {
            loadEnemyMaps();
            var enemyList = new List<string>(EnemyNames.Keys);
            var i = rnd.Next(enemyList.Count);
            var enemy = enemyList[i];
            var count = 5;
            var quest = new EnemyKillQuest($"Defeat {count} {enemy}", EnemyNames[enemy], count);
            quest.Enable();
        }

        private void Pipe_OnReady(object sender, EventArgs e)
        {
            pipe.ClientApi.ClientManager.ConnectEvent += ClientManager_ConnectEvent;
            pipe.ClientApi.ClientManager.DisconnectEvent += ClientManager_DisconnectEvent;
        }

        private void ClientManager_DisconnectEvent()
        {
            // to be used later
        }

        private void ClientManager_ConnectEvent()
        {
            createNewQuests();
        }
    }
}