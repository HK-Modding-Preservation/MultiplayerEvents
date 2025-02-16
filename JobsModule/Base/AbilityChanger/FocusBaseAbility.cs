using AbilityChanger.Base;
using static AbilityChanger.AbilityChanger;

namespace MultiplayerEvents.JobsModule.Base.AbilityChanger
{
    public class FocusBaseAbility : Focus
    {
        protected BaseAbilities parent;
        public FocusBaseAbility(BaseAbilities parent)
        {
            this.parent = parent;
            RegisterTrigger(parent.OnTriggerLocal, true);
            RegisterAbility(this);
        }

        public override string name { get => parent.GetName(); set { } }
        public override string title { get => parent.GetName(); set { } }
        public override string description { get => parent.GetDescription(); set { } }
        public override Sprite activeSprite { get => parent.GetAbilityIcon(); set { } }

    }
}
