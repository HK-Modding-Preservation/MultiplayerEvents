namespace MultiplayerEvents.JobsModule.Objects.Base
{
    public interface GameObjectManager
    {
        public IPrefabFactory Factory { get; }

        public GameObject GetInstance();

        public void ReturnInstance(GameObject go);
    }
}
