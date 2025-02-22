using MultiplayerEvents.JobsModule.Base.Skills;
using MultiplayerEvents.JobsModule.Objects.AreaOfEffect;
using MultiplayerEvents.JobsModule.Objects.Base;
using MultiplayerEvents.JobsModule.Objects.GameObjectManagers;
using MultiplayerEvents.JobsModule.Objects.Trigger;

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
        private void AreaTrigger(Collider2D other)
        {
            HeroController.instance.AddHealth(1);
        }
        public AreaOfEffectConfig GetConfigFor(GameObject parent, bool isLocal)
        {
            return (new AreaOfEffectConfig
            {
                AutoRemove = false,
                AutoRemoveSeconds = 1,
                TriggerConfig = new TriggerConfig { MultiTrigger = false, Layer = GlobalEnums.PhysLayers.HERO_DETECTOR },
                OnTrigger = isLocal ? null : AreaTrigger,
                Parent = parent,
                Xoffset = 0,
                Yoffset = 0
            });
        }
        public override void OnTriggerLocal()
        {
            pipe.Broadcast(AbilityId, "", true, true);
            //spawn heal radius at same position as HeroController.instance.gameObject
            AreaOfEffect.CreateEffect(GetConfigFor(HeroController.instance.gameObject, true));
        }

        public override void OnTriggerRemote(EventContainer data)
        {
            var player = pipe.ClientApi.ClientManager.GetPlayer(data.FromPlayer);
            //spawn heal radius at same position as player.PlayerObject
            AreaOfEffect.CreateEffect(GetConfigFor(player.PlayerObject, false));
        }

    }
}
