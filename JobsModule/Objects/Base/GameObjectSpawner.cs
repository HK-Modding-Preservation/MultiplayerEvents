namespace MultiplayerEvents.JobsModule.Objects.Base
{
    public abstract class GameObjectSpawner(GameObjectManager _manager)
    {
        private GameObjectManager Manager { get; } = _manager;
        protected GameObjectContainer SpawnAt(GameObject parent, float Xoffset, float Yoffset)
        {
            var container = new GameObjectContainer(Manager, Manager.GetInstance());
            container.SetTransform(parent, Xoffset, Yoffset);
            return container;
        }

    }
}
