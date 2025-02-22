using MultiplayerEvents.JobsModule.Abilities.MinorHeal;
using MultiplayerEvents.JobsModule.Base.Skills;
using MultiplayerEvents.JobsModule.Skills.MinorInvisibility;

namespace MultiplayerEvents.JobsModule
{
    internal static class AbilityStore
    {
        // default kit swaps based on job
        internal static BaseSkill MinorHeal = new MinorHealSkill();
        internal static BaseSkill MinorInvisibility = new MinorInvisibilitySkill();
        internal static BaseSkill MinorInvulnerability;

    }
}
