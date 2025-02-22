using SFCore;

namespace MultiplayerEvents.JobsModule.Base.Charms
{
    public class BaseSkillCharm(IBaseSkillCharmContainer parent) : EasyCharm
    {
        private IBaseSkillCharmContainer Parent { get; } = parent;

        protected override int GetCharmCost() => Parent.GetCost();

        protected override string GetDescription() => Parent.GetDescription();

        protected override string GetName() => Parent.GetName();

        protected override Sprite GetSpriteInternal() => Parent.GetIcon();

        public void Equip()
        {
            RestoreCharmState(new EasyCharmState { IsEquipped = true, GotCharm = GotCharm, IsNew = IsNew });
        }
        public void UnEquip()
        {
            RestoreCharmState(new EasyCharmState { IsEquipped = false, GotCharm = GotCharm, IsNew = IsNew });
        }
    }
}
