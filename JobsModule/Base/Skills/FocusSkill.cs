using MultiplayerEvents.JobsModule.Base.AbilityChanger;

namespace MultiplayerEvents.JobsModule.Base.Skills
{
    public abstract class FocusSkill : BaseSkill
    {
        public FocusSkill()
        {
            Ability = new FocusBaseAbility(this);
        }
    }
}

