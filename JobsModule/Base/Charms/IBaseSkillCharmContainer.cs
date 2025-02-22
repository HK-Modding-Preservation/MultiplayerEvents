namespace MultiplayerEvents.JobsModule.Base.Charms
{
    public interface IBaseSkillCharmContainer
    {
        public string GetName();
        public string GetDescription();
        public Sprite GetIcon();
        public int GetCost();
    }
}
