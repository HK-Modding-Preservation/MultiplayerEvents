using MultiplayerEvents.JobsModule.Base.Skills;
using SFCore;

namespace MultiplayerEvents.JobsModule
{
    public class SkillManager
    {

        public List<BaseSkill> AvailableAbilities = [];

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
            AvailableAbilities = new List<BaseSkill>()
            {
                AbilityStore.MinorHeal,
                AbilityStore.MinorInvisibility,
                AbilityStore.MinorInvulnerability
            };
            foreach (BaseSkill ability in AvailableAbilities)
            {
                ability.Charm.GiveCharm();
            }
            On.HeroController.CharmUpdate += HeroController_CharmUpdate;
        }

        private void HeroController_CharmUpdate(On.HeroController.orig_CharmUpdate orig, HeroController self)
        {
            MultiplayerEvents.LocalSettings.CharmStates = GetCharmStates();
        }

    }
}
