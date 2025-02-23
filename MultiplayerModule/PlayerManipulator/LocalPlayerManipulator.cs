namespace MultiplayerEvents.MultiplayerModule.PlayerManipulator
{
    public class LocalPlayerManipulator : ILocalPlayerManipulator
    {
        public HeroController Hc => HeroController.instance;

        public static LocalPlayerManipulator Instance = new LocalPlayerManipulator();
        public void AddHealth(int amount)
        {
            Hc.AddHealth(amount);
        }

        public GameObject GetMainObject()
        {
            return Hc.gameObject;
        }

        public void MakeInvisible()
        {
            Hc.gameObject.GetComponent<Renderer>().enabled = false;
        }

        public void MakeVisible()
        {
            Hc.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
