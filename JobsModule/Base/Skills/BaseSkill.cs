using AbilityChanger;
using MultiplayerEvents.JobsModule.Base.AbilityChanger;
using MultiplayerEvents.JobsModule.Base.Charms;
using MultiplayerEvents.JobsModule.Base.PlayerManipulator;
using MultiplayerEvents.MultiplayerModule;

namespace MultiplayerEvents.JobsModule.Base.Skills
{
    public abstract class BaseSkill : IAbilityContainer, IBaseSkillCharmContainer
    {
        public PipeClient pipe { get => MainPipe.Instance.pipe; }
        public BaseSkillCharm Charm { get; protected set; }
        public Ability Ability { get; protected set; }
        public abstract string AbilityId { get; }
        public abstract int GetCost();
        public abstract Sprite GetIcon();
        public abstract void OnTriggerLocal(ILocalPlayerManipulator player);
        public abstract void OnTriggerRemote(IPlayerManipulator player, EventContainer data);
        public virtual string GetName() => $"{AbilityId}";
        public abstract string GetDescription();
        public virtual Sprite GetAbilityIcon() => GetIcon();

        private bool lastEquipState = false;

        public void OnTriggerLocal()
        {
            OnTriggerLocal(LocalPlayerManipulator.Instance);
        }
        protected BaseSkill()
        {
            Charm = new BaseSkillCharm(this);
            pipe.OnRecieve += Pipe_OnRecieve;
            On.HeroController.CharmUpdate += HeroController_CharmUpdate;
        }

        private void OnCharmUpdate()
        {
            if (lastEquipState == Charm.IsEquipped) { return; }
            if (Charm.IsEquipped)
            {
                Ability?.setAquireAbility(true, true);
            }
            else
            {
                Ability?.setAquireAbility(false);
            }
            lastEquipState = Charm.IsEquipped;
        }
        private void HeroController_CharmUpdate(On.HeroController.orig_CharmUpdate orig, HeroController self)
        {
            OnCharmUpdate();
            orig(self);
        }

        private void Pipe_OnRecieve(object sender, ReceivedEventArgs e)
        {
            if (e.Data.EventName == AbilityId)
            {
                var player = pipe.ClientApi.ClientManager.GetPlayer(e.Data.FromPlayer);
                OnTriggerRemote(new RemotePlayerManipulator(player), e.Data);
            }
        }

    }
}

