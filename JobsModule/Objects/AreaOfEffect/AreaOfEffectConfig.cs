using MultiplayerEvents.JobsModule.Objects.Base;
using MultiplayerEvents.JobsModule.Objects.Trigger;

namespace MultiplayerEvents.JobsModule.Objects.AreaOfEffect
{
    public class AreaOfEffectConfig : IEffectConfig
    {
        public TriggerConfig TriggerConfig;
        public bool AutoRemove;
        public float AutoRemoveSeconds;
        public GameObject Parent;
        public float Xoffset;
        public float Yoffset;
        public Action<Collider2D>? OnTrigger;

    }
}
