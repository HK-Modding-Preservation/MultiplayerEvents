using MultiplayerEvents.JobsModule.Base;

namespace MultiplayerEvents.JobsModule.Jobs
{
    internal class Charger : BaseJob
    {
        List<BaseAbilities> _abilities = new List<BaseAbilities>() { AbilityStore.MinorInvulnerability };
        public override List<BaseAbilities> GetAbilities() => _abilities;
        protected override string GetDescription() => FlavorTexts.ChargerDescription;
        protected override string GetName() => FlavorTexts.ChargerName;
        protected override Sprite GetSpriteInternal() => AssetManager.ChargerSprite;
    }
}
