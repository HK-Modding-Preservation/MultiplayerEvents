using MultiplayerEvents.BaseModule;
using MultiplayerEvents.JobsModule;
using MultiplayerEvents.QuestsModule;

namespace MultiplayerEvents
{
    public partial class MultiplayerEvents : Mod
    {

        internal static MultiplayerEvents Instance;
        private TeamScore teamScore = new();
        private QuestManager questManager = new();
        private PersistentSkulls persistentSkulls = new();
        private DeathLink deathLink = new();
        private SkillManager skillManager = new();

        public MultiplayerEvents()
        {
            Instance = this;
        }
        public override void Initialize()
        {
            teamScore.Init();
            questManager.Init();
            persistentSkulls.Init();
            deathLink.Init();
            skillManager.Init();
        }


    }
}