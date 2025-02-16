using AbilityChanger;
using MultiplayerEvents.JobsModule.Base;
using MultiplayerEvents.JobsModule.Base.AbilityChanger;

namespace MultiplayerEvents.JobsModule.Abilities.MinorHeal
{
    public class MinorHeal : BaseAbilities
    {
        public override string AbilityId => "MinorHeal";
        public override int GetCost() => 1;
        public override Sprite GetIcon() => AssetManager.MinorHealSprite;
        public override string GetDescription() => FlavorTexts.MinorHealDescription;
        protected override Ability ConstructAbility() => new FocusBaseAbility(this);

        public MinorhealAreaOfEffectFactory Area = new MinorhealAreaOfEffectFactory();
        public override void OnTriggerLocal()
        {
            pipe.Broadcast(AbilityId, "", true, true);
            //spwan heal radius at same position as player.PlayerObject
            Area.Spawn(HeroController.instance.gameObject, 0, 0);
        }

        public override void OnTriggerRemote(EventContainer data)
        {
            var player = pipe.ClientApi.ClientManager.GetPlayer(data.FromPlayer);
            //spwan heal radius at same position as player.PlayerObject
            Area.Spawn(player.PlayerObject, 0, 0);
        }

    }
}
