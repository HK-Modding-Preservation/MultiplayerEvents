using MultiplayerEvents.JobsModule.Base.PlayerManipulator;
using MultiplayerEvents.JobsModule.Base.Skills;
using MultiplayerEvents.JobsModule.Objects.AreaOfEffect;
using MultiplayerEvents.JobsModule.Objects.Base;
using MultiplayerEvents.JobsModule.Objects.GameObjectManagers;
using MultiplayerEvents.JobsModule.Objects.Trigger;
using UnityEngine.SocialPlatforms;

namespace MultiplayerEvents.JobsModule.Abilities.MinorHeal
{
    public class MinorHealSkill : FocusSkill
    {
        public override string AbilityId => "MinorHeal";
        public override int GetCost() => 1;
        public override Sprite GetIcon() => AssetManager.MinorHealSprite;
        public override string GetDescription() => FlavorTexts.MinorHealDescription;

        private AreaOfEffectFactory AreaOfEffect;
        public MinorHealSkill()
        {
            PrefabFactory _prefabFactory = new();
            GameObjectManager _manager = new AlwaysFreshGameObjectManager(_prefabFactory);
            AreaOfEffect = new AreaOfEffectFactory(_manager);
        }
        private void AreaTrigger(IPlayerManipulator player)
        {
            if (player is ILocalPlayerManipulator localPlayer)
            {
                localPlayer.AddHealth(1);
            }
        }
        public AreaOfEffectConfig GetConfigFor(IPlayerManipulator player)
        {
            return (new AreaOfEffectConfig
            {
                AutoRemove = false,
                AutoRemoveSeconds = 1,
                TriggerConfig = new TriggerConfig { MultiTrigger = false, Layer = GlobalEnums.PhysLayers.HERO_DETECTOR },
                OnTrigger = ((Collider2D other) => AreaTrigger(player)),
                Parent = player.GetMainObject(),
                Xoffset = 0,
                Yoffset = 0
            });
        }
        public override void OnTriggerLocal(ILocalPlayerManipulator player)
        {
            pipe.Broadcast(AbilityId, "", true, true);
            //spawn heal radius at same position as HeroController.instance.gameObject
            AreaOfEffect.CreateEffect(GetConfigFor(player));
        }

        public override void OnTriggerRemote(IPlayerManipulator player, EventContainer data)
        {
            //spawn heal radius at same position as player.PlayerObject
            AreaOfEffect.CreateEffect(GetConfigFor(player));
        }

    }
}
