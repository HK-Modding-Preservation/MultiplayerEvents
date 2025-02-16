using HKMirror.Reflection.SingletonClasses;
using MultiplayerEvents.MultiplayerModule;

namespace MultiplayerEvents.BaseModule
{
    public class DeathLink
    {
        private PipeClient pipe { get => MainPipe.Instance.pipe; }
        private bool DeathLinkRoom { get => MultiplayerEvents.Settings.DeathLinkRoom; }
        private bool DeathLinkEnabled { get => MultiplayerEvents.Settings.DeathLinkEnabled; }
        private bool DeathLinkTeam { get => MultiplayerEvents.Settings.DeathLinkTeam; }

        private string DeathEventName = "DeathLink";
        private bool isDeathLinkDeath = false;

        public void Init()
        {
            pipe.OnReady += Pipe_OnReady;
        }

        private void Pipe_OnReady(object sender, EventArgs e)
        {

            ModHooks.BeforePlayerDeadHook += ModHooks_BeforePlayerDeadHook;
            ModHooks.AfterPlayerDeadHook += ModHooks_AfterPlayerDeadHook;
            pipe.OnRecieve += Pipe_OnRecieve;
        }

        private void Pipe_OnRecieve(object sender, ReceivedEventArgs e)
        {
            if (e.Data.EventName != DeathEventName) { return; }
            var scene = e.Data.SceneName;
            var player = pipe.ClientApi.ClientManager.GetPlayer(e.Data.FromPlayer);
            var sameTeam = player.Team == pipe.ClientApi.ClientManager.Team;
            var sameRoom = player.IsInLocalScene;
            if (player == null) { return; }
            if (DeathLinkEnabled)
            {
                if ((!DeathLinkTeam || sameTeam) && (!DeathLinkRoom || sameRoom))
                {
                    KillLocalPlayer();
                }
            }
        }

        private void KillLocalPlayer()
        {
            isDeathLinkDeath = true;
            PlayerData.instance.TakeHealth(int.MaxValue);
            _ = CoroutineHelper.GetRunner().StartCoroutine(HeroControllerR.Die());
        }

        private void ModHooks_BeforePlayerDeadHook()
        {
            if (DeathLinkEnabled && !isDeathLinkDeath)
            {
                pipe.Broadcast(DeathEventName, DeathEventName, false);
            }
        }


        private void ModHooks_AfterPlayerDeadHook()
        {
            isDeathLinkDeath = false;
        }

    }
}
