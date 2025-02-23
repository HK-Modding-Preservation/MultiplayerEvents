namespace MultiplayerEvents.JobsModule.Base.PlayerManipulator
{
    public interface IPlayerManipulator
    {
        public GameObject GetMainObject();
        public void MakeInvisible();
        public void MakeVisible();
    }

    public interface ILocalPlayerManipulator : IPlayerManipulator {

        public void AddHealth(int amount);
    }
}
