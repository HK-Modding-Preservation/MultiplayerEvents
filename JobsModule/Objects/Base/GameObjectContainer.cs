namespace MultiplayerEvents.JobsModule.Objects.Base
{
    public class GameObjectContainer(GameObjectManager Manager, GameObject current)
    {
        public GameObject Current { get; } = current;
        public void Activate()
        {
            Current.SetActive(true);
        }
        public void Despawn()
        {
            Manager.ReturnInstance(Current);
        }
        public void SetTransform(GameObject parent, float Xoffset, float Yoffset)
        {
            Current.transform.parent = parent.transform;
            Current.transform.localPosition = new Vector3(Xoffset, Yoffset, 0);
        }
    }
}
