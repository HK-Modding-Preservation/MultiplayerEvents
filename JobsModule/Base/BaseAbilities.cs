using AbilityChanger;
using MultiplayerEvents.MultiplayerModule;
using SFCore;

namespace MultiplayerEvents.JobsModule.Base
{

    public abstract class BaseAbilities
    {
        private bool lastEquipState = false;
        public PipeClient pipe { get => MainPipe.Instance.pipe; }
        public BaseAbilityCharm Charm { get; protected set; }
        public abstract string AbilityId { get; }
        public abstract int GetCost();
        public abstract Sprite GetIcon();
        public abstract void OnTriggerLocal();
        public abstract void OnTriggerRemote(EventContainer data);
        protected abstract Ability ConstructAbility();
        public virtual string GetName() => $"{AbilityId}";
        public abstract string GetDescription();
        public virtual Sprite GetAbilityIcon() => GetIcon();
        public virtual Ability ability { get; protected set; }

        public virtual void OnEquipped()
        {
            ability?.setAquireAbility(true, true);
        }

        public virtual void OnUnEquipped()
        {
            ability?.setAquireAbility(false, true);
        }
        protected BaseAbilities()
        {
            Charm = new BaseAbilityCharm(this);
            On.HeroController.CharmUpdate += HeroController_CharmUpdate;
            pipe.OnRecieve += Pipe_OnRecieve;
            ability = ConstructAbility();
        }

        private void Pipe_OnRecieve(object sender, ReceivedEventArgs e)
        {
            if (e.Data.EventName == AbilityId)
            {
                OnTriggerRemote(e.Data);
            }
        }

        private void HeroController_CharmUpdate(On.HeroController.orig_CharmUpdate orig, HeroController self)
        {
            OnCharmUpdate();
            orig(self);
        }

        public void Give()
        {
            Charm.GiveCharm();
        }
        public void Take()
        {
            Charm.TakeCharm();
        }
        public void Equip()
        {
            Charm.RestoreCharmState(new EasyCharmState { IsEquipped = true, GotCharm = Charm.GotCharm, IsNew = Charm.IsNew });
        }
        public void UnEquip()
        {
            Charm.RestoreCharmState(new EasyCharmState { IsEquipped = false, GotCharm = Charm.GotCharm, IsNew = Charm.IsNew });
        }
        public void OnCharmUpdate()
        {
            if (lastEquipState == Charm.IsEquipped) { return; }
            if (Charm.IsEquipped)
            {
                OnEquipped();
            }
            else
            {
                OnUnEquipped();
            }
            lastEquipState = Charm.IsEquipped;
        }

    }
}
