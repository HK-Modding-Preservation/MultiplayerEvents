using SFCore;

namespace MultiplayerEvents.JobsModule.Base
{
    public class BaseAbilityCharm : EasyCharm
    {
        public BaseAbilityCharm(string AbilityName, string AbilityDescription, int AbilityCost, Sprite AbilityIcon)
        {
            this.AbilityName = AbilityName;
            this.AbilityDescription = AbilityDescription;
            this.AbilityCost = AbilityCost;
            this.AbilityIcon = AbilityIcon;
        }
        public string AbilityName { get; protected set; }
        public string AbilityDescription { get; protected set; }
        public int AbilityCost { get; protected set; }
        public Sprite AbilityIcon { get; protected set; }

        protected override int GetCharmCost() => AbilityCost;

        protected override string GetDescription() => AbilityDescription;

        protected override string GetName() => AbilityName;

        protected override Sprite GetSpriteInternal() => AbilityIcon;
    }
    public abstract class BaseAbilities
    {
        public abstract string GetName();
        public abstract string GetDescription();
        public abstract int GetCost();
        public abstract Sprite GetIcon();
        public BaseAbilityCharm Charm { get; protected set; }
        protected BaseAbilities()
        {
            Charm = new BaseAbilityCharm(GetName(), GetDescription(), GetCost(), GetIcon());
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

    }
}
