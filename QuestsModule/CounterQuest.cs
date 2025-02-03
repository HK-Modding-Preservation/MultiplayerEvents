namespace MultiplayerEvents.QuestsModule
{
    public abstract class CounterQuest
    {
        public string Name = "Quest_Name";
        public int Count = 0;
        public int MaxValue = 1;
        public bool Enabled = false;
        public void Enable()
        {
            if (Enabled) { return; }
            Enabled = true;
            OnEnable();
        }
        public void Disable()
        {
            if (!Enabled) { return; }
            Enabled = false;
            OnDisable();
        }

        public virtual void OnEnable() { }
        public virtual void OnDisable() { }

    }
}