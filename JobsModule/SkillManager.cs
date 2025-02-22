using MultiplayerEvents.JobsModule.Base.Skills;

namespace MultiplayerEvents.JobsModule
{
    public class SkillManager
    {

        public List<BaseSkill> AvailableAbilities = new List<BaseSkill>()
            {
                AbilityStore.MinorHeal,
                AbilityStore.MinorInvisibility
            };

        internal void Init()
        {

            foreach (BaseSkill ability in AvailableAbilities)
            {
                ability.Charm.GiveCharm();
            }
        }

    }
}
