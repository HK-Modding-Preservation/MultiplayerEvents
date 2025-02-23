namespace MultiplayerEvents.JobsModule.Objects.Base
{
    public interface IEffectFactory<T> where T : BaseEffectConfig
    {
        public void Decorate(GameObjectContainer container, T config);
        public GameObjectContainer CreateEffect(T config);

    }
}
