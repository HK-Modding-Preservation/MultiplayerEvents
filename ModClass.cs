using MultiplayerEvents.BaseModule;
using MultiplayerEvents.JobsModule;
using MultiplayerEvents.QuestsModule;

namespace MultiplayerEvents
{
    public partial class MultiplayerEvents : Mod, IGlobalSettings<GlobalModSettings>, ICustomMenuMod
    {

        internal static MultiplayerEvents Instance;

        public override void Initialize()
        {
            Instance = this;

            var teamScore = new TeamScore();
            teamScore.Init();

            var questManager = new QuestManager();
            questManager.Init();

            var persistentSkulls = new PersistentSkulls();
            persistentSkulls.Init();

            var deathLink = new DeathLink();
            deathLink.Init();
            new JobManager().Init();
        }


    }
}