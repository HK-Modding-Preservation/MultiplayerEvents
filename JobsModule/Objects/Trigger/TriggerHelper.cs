namespace MultiplayerEvents.JobsModule.Objects.Trigger
{
    public class TriggerHelper : MonoBehaviour
    {

        public Action<Collider2D> onTrigger;
        public TriggerConfig config;
        private bool triggered = false;
        public void OnTriggerEnter2D(Collider2D Other)
        {
            if (config.MultiTrigger || !triggered)
            {
                onTrigger?.Invoke(Other);
                triggered = true;
            }
        }
    }
}
