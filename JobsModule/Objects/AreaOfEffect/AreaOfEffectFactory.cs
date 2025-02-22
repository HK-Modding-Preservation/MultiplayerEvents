using MultiplayerEvents.JobsModule.Objects.Base;
using MultiplayerEvents.JobsModule.Objects.Trigger;

namespace MultiplayerEvents.JobsModule.Objects.AreaOfEffect
{
    public class AreaOfEffectFactory(GameObjectManager _manager) : GameObjectSpawner(_manager), IEffectFactory<AreaOfEffectConfig>
    {

        public GameObjectContainer CreateEffect(AreaOfEffectConfig config)
        {
            var container = SpawnAt(config.Parent, config.Xoffset, config.Yoffset);
            Decorate(container, config);
            container.Activate();
            return container;
        }

        public void Decorate(GameObjectContainer container, AreaOfEffectConfig config)
        {
            container.Current.layer = (int)config.TriggerConfig.Layer;
            var triggerHelper = container.Current.GetAddComponent<TriggerHelper>();
            triggerHelper.config = config.TriggerConfig;
            triggerHelper.onTrigger = config.OnTrigger;
            if (config.AutoRemove)
            {
                CoroutineHelper.WaitForSecondsBeforeInvoke(config.AutoRemoveSeconds, () => container.Despawn());
            }
        }

    }
}
