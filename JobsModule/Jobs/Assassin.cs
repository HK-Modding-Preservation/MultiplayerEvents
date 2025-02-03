using MultiplayerEvents.JobsModule.Base;

namespace MultiplayerEvents.JobsModule.Jobs
{
    internal class Assassin : BaseJob
    {
        List<BaseAbilities> _abilities = new List<BaseAbilities>() { AbilityStore.MinorInvisibility };
        public override List<BaseAbilities> GetAbilities() => _abilities;
        protected override string GetDescription() => FlavorTexts.AssassinDescription;
        protected override string GetName() => FlavorTexts.AssassinName;
        protected override Sprite GetSpriteInternal() => AssetManager.AssassinSprite;
    }
}
