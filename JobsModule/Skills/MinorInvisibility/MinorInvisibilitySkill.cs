using MultiplayerEvents.JobsModule.Base.PlayerManipulator;
using MultiplayerEvents.JobsModule.Base.Skills;

namespace MultiplayerEvents.JobsModule.Skills.MinorInvisibility
{
    public class MinorInvisibilitySkill : FocusSkill
    {
        public override string AbilityId => "MinorInvisibilitySkill";

        public override int GetCost() => 1;

        public override string GetDescription() => FlavorTexts.AssassinDescription;

        public override Sprite GetIcon() => AssetManager.AssassinSprite;

        public float SkillDuration = 5f;

        public void ApplySkill(IPlayerManipulator player)
        {
            player.MakeInvisible();
            CoroutineHelper.WaitForSecondsBeforeInvoke(SkillDuration, () =>
            {
                player.MakeVisible();
            });
        }
        public override void OnTriggerLocal(ILocalPlayerManipulator player)
        {
            pipe.Broadcast(AbilityId, "", true, true);
            ApplySkill(player);
        }

        public override void OnTriggerRemote(IPlayerManipulator player,EventContainer data)
        {
            ApplySkill(player);
        }
    }
}
