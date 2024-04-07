

namespace MultiplayerEvents
{
    public class MultiplayerEvents : Mod
    {
        public override string GetVersion() => Satchel.AssemblyUtils.GetAssemblyVersionHash();
        internal static MultiplayerEvents Instance;
        internal PipeClient pipe = new PipeClient("MultiplayerEvents");
        internal Dictionary<int,TeamData> Teams = new();
        internal TeamData CurrentTeam;
        private bool listening = false;
        private int lastTeam = -1;

        public override void Initialize()
        {
            Instance = this;
     
            ModHooks.OnReceiveDeathEventHook += ModHooks_OnReceiveDeathEventHook;
            ModHooks.AfterPlayerDeadHook += ModHooks_AfterPlayerDeadHook;
            ModHooks.HeroUpdateHook += ModHooks_HeroUpdateHook;
            GUI.Init();
            PersistentSkulls.Hook();
        }

        private void ModHooks_HeroUpdateHook()
        {
            ListenToClientEvents();
            if (pipe != null && pipe.ClientApi != null && lastTeam != (int)pipe.ClientApi.ClientManager.Team)
            {
                var team = ((int)pipe.ClientApi.ClientManager.Team);
                if(Teams.TryGetValue(team,out var t)){
                    CurrentTeam = Teams[team];
                    lastTeam = team;
                    GUI.UpdateText();
                }
            }
        }

        private void ListenToClientEvents()
        {
            if (listening) { return;  }
            if (pipe == null || pipe.ClientApi == null) { return; }
            pipe.ClientApi.ClientManager.ConnectEvent += ClientManager_ConnectEvent;
            pipe.ClientApi.ClientManager.DisconnectEvent += ClientManager_DisconnectEvent;
            listening = true;
        }

        private void ClientManager_DisconnectEvent()
        {

            LogDebug("disconnect");
            var Teamids = Enum.GetValues(typeof(Hkmp.Game.Team));
            foreach(int team in Teamids){
                Teams[team]?.DestroyCounters();
            }
        }

        private void ClientManager_ConnectEvent()
        {
            LogDebug("connect");
            var Teamids = Enum.GetValues(typeof(Hkmp.Game.Team));
            var TeamNames = Enum.GetNames(typeof(Hkmp.Game.Team));
            var i = 0;
            foreach(int team in Teamids){
                Teams[team] = new TeamData(TeamNames[i],team);
                i++;
            }
            QuestManager.createNewQuests();
        }

        private void ModHooks_AfterPlayerDeadHook()
        {
            CurrentTeam.Score.Decrement(5);
        }

        private void ModHooks_OnReceiveDeathEventHook(EnemyDeathEffects enemyDeathEffects, bool eventAlreadyReceived, ref float? attackDirection, ref bool resetDeathEvent, ref bool spellBurn, ref bool isWatery)
        {
            if(eventAlreadyReceived) { return; }
            CurrentTeam.KillCount.Increment(1);
            ushort points = 1;
            var enemyName = enemyDeathEffects.gameObject.GetName(true);
            foreach (var enemy in EnemyDatabase.hardEnemyTypeNames)
            {
                if (enemy.ToLower() == enemyName.ToLower())
                {
                    points += 4;
                }
            }
            foreach (var enemy in EnemyDatabase.bigEnemyTypeNames)
            {
                if(enemy.ToLower() == enemyName.ToLower())
                {
                    points += 1;
                }
            }
            CurrentTeam.Score.Increment(points);
        }
    }
}