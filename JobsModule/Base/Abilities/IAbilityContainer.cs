namespace MultiplayerEvents.JobsModule.Base.AbilityChanger
{
    public interface IAbilityContainer
    {
        public void OnTriggerLocal();
        public string GetName();
        public string GetDescription();
        public Sprite GetAbilityIcon();

    }
}

