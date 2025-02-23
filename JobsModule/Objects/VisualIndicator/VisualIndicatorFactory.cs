using MultiplayerEvents.JobsModule.Objects.Base;

namespace MultiplayerEvents.JobsModule.Objects.VisualIndicatorFactory
{
    public class VisualIndicatorFactory(GameObjectManager _manager) : GameObjectSpawner(_manager), IEffectFactory<BaseEffectConfig>
    {
        public GameObjectContainer CreateEffect(BaseEffectConfig config)
        {
            var container = SpawnAt(config.Parent, config.Xoffset, config.Yoffset);
            Decorate(container, config);
            container.Activate();
            return container;
        }

        public void Decorate(GameObjectContainer container, BaseEffectConfig config)
        {

        }

    }
}
