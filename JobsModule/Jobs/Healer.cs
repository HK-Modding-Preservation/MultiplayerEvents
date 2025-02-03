using MultiplayerEvents.JobsModule.Base;

namespace MultiplayerEvents.JobsModule
{
    internal class Healer : BaseJob
    {
        List<BaseAbilities> _abilities = new List<BaseAbilities>() { AbilityStore.MinorHeal };
        public override List<BaseAbilities> GetAbilities() => _abilities;
        protected override string GetDescription() => FlavorTexts.HealerDescription;
        protected override string GetName() => FlavorTexts.HealerName;
        protected override Sprite GetSpriteInternal() => AssetManager.HealerSprite;
    }
}
