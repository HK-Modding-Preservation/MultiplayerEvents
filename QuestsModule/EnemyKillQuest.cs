using MultiplayerEvents.MultiplayerModule;

namespace MultiplayerEvents.QuestsModule
{
    public class EnemyKillQuest : CounterQuest
    {
        public List<string> EnemyName;
        public EnemyKillQuest(string Name, List<string> EnemyName, int MaxValue) : base()
        {
            this.Name = Name;
            this.EnemyName = EnemyName;
            this.MaxValue = MaxValue;
        }
        public override void OnEnable()
        {
            Chat.AddMessage($"Acquired Quest : {Name}");
            ModHooks.OnReceiveDeathEventHook += ModHooks_OnReceiveDeathEventHook;
        }
        public override void OnDisable()
        {
            ModHooks.OnReceiveDeathEventHook -= ModHooks_OnReceiveDeathEventHook;
        }
        private void ModHooks_OnReceiveDeathEventHook(EnemyDeathEffects enemyDeathEffects, bool eventAlreadyReceived, ref float? attackDirection, ref bool resetDeathEvent, ref bool spellBurn, ref bool isWatery)
        {
            if (eventAlreadyReceived) { return; }
            var enemyName = enemyDeathEffects.gameObject.GetName(true);
            if (EnemyName.Contains(enemyName))
            {
                Count++;
            }
            if (Count >= MaxValue)
            {
                Disable();
                Chat.AddMessage($"Completed Quest : {Name}");
            }
        }
    }
}