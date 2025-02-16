namespace MultiplayerEvents.JobsModule.Objects
{
    public class AreaOfEffectMonobehaviour : MonoBehaviour
    {

        public Action onHeroTrigger;
        public Action<AreaOfEffectMonobehaviour> onAoeRemove;
        public bool multiTrigger = false;
        private bool triggered = false;
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (multiTrigger || !triggered)
            {
                onHeroTrigger();
                triggered = true;
            }
        }

        public void Remove()
        {
            onAoeRemove(this);
        }
    }

    public class AreaOfEffectObjectManager(GameObject prefab)
    {
        public GameObject Prefab { get; protected set; } = prefab;

        public GameObject GetFreshInstance()
        {
            //Object pooling later
            return GameObject.Instantiate(Prefab);
        }

        public void RemoveInstance(GameObject go)
        {
            //object pooling later
            GameObject.Destroy(go);
        }
    }
    
    public class AreaOfEffectConfig
    {
        public bool AutoRemove;
        public bool MultiTrigger;
        public float AutoRemoveSeconds;
    }
    public abstract class BaseAreaOfEffectFactory
    {
        public abstract AreaOfEffectConfig Config { get; }
        public abstract void OnHeroTrigger();

        public AreaOfEffectObjectManager spawner;
        public BaseAreaOfEffectFactory()
        {
        }
        public AreaOfEffectMonobehaviour Spawn(GameObject parent, float xOffset, float yOffset)
        {
            var instance = spawner.GetFreshInstance();
            instance.SetActive(false);
            instance.transform.parent = parent.transform;
            instance.transform.localPosition = new Vector3(xOffset, yOffset, 0);
            var Aoe = instance.GetAddComponent<AreaOfEffectMonobehaviour>();
            Aoe.multiTrigger = Config.MultiTrigger;
            Aoe.onHeroTrigger = OnHeroTrigger;
            Aoe.onAoeRemove = Remove;
            instance.SetActive(true);
            if (Config.AutoRemove)
            {
                CoroutineHelper.WaitForSecondsBeforeInvoke(Config.AutoRemoveSeconds, () => Remove(Aoe));
            }
            return Aoe;
        }

        public void Remove(AreaOfEffectMonobehaviour Aoe)
        {
            spawner.RemoveInstance(Aoe.gameObject);
        }
    }
}
