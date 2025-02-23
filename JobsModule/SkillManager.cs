using MultiplayerEvents.JobsModule.Base.Skills;
using SFCore;

namespace MultiplayerEvents.JobsModule
{
    public class SkillManager
    {

        public List<BaseSkill> AvailableAbilities = new List<BaseSkill>()
            {
                AbilityStore.MinorHeal,
                AbilityStore.MinorInvisibility
            };

        internal Dictionary<string, EasyCharmState> GetCharmStates()
        {
            Dictionary<string, EasyCharmState> charmStates = [];
            foreach (BaseSkill ability in AvailableAbilities)
            {
                charmStates.Add(ability.AbilityId, ability.Charm.GetCharmState());
            }
            return charmStates;
        }

        internal void RestoreCharmStates(Dictionary<string, EasyCharmState> charmStates)
        {
            foreach (BaseSkill ability in AvailableAbilities)
            {
                if (charmStates.TryGetValue(ability.AbilityId, out var charmState))
                {
                    ability.Charm.RestoreCharmState(charmState);
                }
            }
        }

        internal void Init()
        {

            foreach (BaseSkill ability in AvailableAbilities)
            {
                ability.Charm.GiveCharm();
            }
        }

    }
}
