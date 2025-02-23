namespace MultiplayerEvents.MultiplayerModule.PlayerManipulator
{
    public interface IPlayerManipulator
    {
        public GameObject GetMainObject();
        public void MakeInvisible();
        public void MakeVisible();
    }

    public interface ILocalPlayerManipulator : IPlayerManipulator
    {

        public void AddHealth(int amount);
        public void HealEffect();

        public void DamageEvasionEffect();
        public void MakeInvulnerable();
        public void MakeVulnerable();
    }
}
