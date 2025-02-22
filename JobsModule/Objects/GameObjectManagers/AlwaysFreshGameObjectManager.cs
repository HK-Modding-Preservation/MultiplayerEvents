using MultiplayerEvents.JobsModule.Objects.Base;

namespace MultiplayerEvents.JobsModule.Objects.GameObjectManagers
{
    public class AlwaysFreshGameObjectManager(IPrefabFactory factory) : GameObjectManager
    {
        public IPrefabFactory Factory { get; } = factory;

        public GameObject _prefab;
        public GameObject GetInstance()
        {
            if (_prefab == null)
            {
                _prefab = Factory.BuildPersistentPrefab();
            }
            return GameObject.Instantiate(_prefab);
        }

        public void ReturnInstance(GameObject go)
        {
            UnityEngine.Object.Destroy(go);
        }
    }
}
