using MultiplayerEvents.EnemyModule;
using MultiplayerEvents.MultiplayerModule;

namespace MultiplayerEvents.BaseModule
{
    public class TeamScore
    {
        public string team;

        internal Dictionary<int, TeamData> Teams = new();
        internal TeamData CurrentTeam;
        private int lastTeam = -1;
        private PipeClient pipe { get => MainPipe.Instance.pipe; }
        internal TeamScoreUI ui = new TeamScoreUI();
        public void Init()
        {
            On.HeroController.Awake += HeroController_Awake;
            ModHooks.OnReceiveDeathEventHook += ModHooks_OnReceiveDeathEventHook;
            ModHooks.AfterPlayerDeadHook += ModHooks_AfterPlayerDeadHook;
            ModHooks.HeroUpdateHook += ModHooks_HeroUpdateHook;
            pipe.OnReady += Pipe_OnReady;
        }

        private void HeroController_Awake(On.HeroController.orig_Awake orig, HeroController self)
        {
            ui.Init();
            orig(self);
        }
        private void Pipe_OnReady(object sender, EventArgs e)
        {
            pipe.ClientApi.ClientManager.ConnectEvent += ClientManager_ConnectEvent;
            pipe.ClientApi.ClientManager.DisconnectEvent += ClientManager_DisconnectEvent;
        }

        private void ClientManager_DisconnectEvent()
        {

            MultiplayerEvents.Instance.LogDebug("disconnect");
            var Teamids = Enum.GetValues(typeof(Hkmp.Game.Team));
            foreach (int team in Teamids)
            {
                Teams[team]?.DestroyCounters();
            }
        }

        private void ClientManager_ConnectEvent()
        {
            MultiplayerEvents.Instance.LogDebug("connect");
            var Teamids = Enum.GetValues(typeof(Hkmp.Game.Team));
            var TeamNames = Enum.GetNames(typeof(Hkmp.Game.Team));
            var i = 0;
            foreach (int team in Teamids)
            {
                Teams[team] = new TeamData(pipe, ui, TeamNames[i], team);
                i++;
            }
        }
        private void ModHooks_HeroUpdateHook()
        {
            if (pipe != null && pipe.ClientApi != null && lastTeam != (int)pipe.ClientApi.ClientManager.Team)
            {
                var team = ((int)pipe.ClientApi.ClientManager.Team);
                if (Teams.TryGetValue(team, out var t))
                {
                    CurrentTeam = Teams[team];
                    lastTeam = team;
                    ui.UpdateText(CurrentTeam);
                }
            }
        }





        private void ModHooks_AfterPlayerDeadHook()
        {
            CurrentTeam.Score.Decrement(5);
        }

        private void ModHooks_OnReceiveDeathEventHook(EnemyDeathEffects enemyDeathEffects, bool eventAlreadyReceived, ref float? attackDirection, ref bool resetDeathEvent, ref bool spellBurn, ref bool isWatery)
        {
            if (eventAlreadyReceived) { return; }
            if (CurrentTeam == null) { return; }
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
                if (enemy.ToLower() == enemyName.ToLower())
                {
                    points += 1;
                }
            }
            CurrentTeam.Score.Increment(points);
        }
    }
}
