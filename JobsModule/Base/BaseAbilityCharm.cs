using SFCore;

namespace MultiplayerEvents.JobsModule.Base
{
    public class BaseAbilityCharm(BaseAbilities parent) : EasyCharm
    {
        public BaseAbilities Parent { get; } = parent;

        protected override int GetCharmCost() => Parent.GetCost();

        protected override string GetDescription() => Parent.GetDescription();

        protected override string GetName() => Parent.GetName();

        protected override Sprite GetSpriteInternal() => Parent.GetIcon();
    }
}
