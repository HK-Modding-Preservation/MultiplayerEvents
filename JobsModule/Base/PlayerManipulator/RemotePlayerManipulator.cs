using Hkmp.Api.Client;

namespace MultiplayerEvents.JobsModule.Base.PlayerManipulator
{
    public class RemotePlayerManipulator(IClientPlayer player) : IPlayerManipulator
    {

        public GameObject GetMainObject()
        {
            return player.PlayerObject;
        }

        public void MakeInvisible()
        {
            player.PlayerContainer.SetActive(false);
        }

        public void MakeVisible()
        {
            player.PlayerContainer.SetActive(true);
        }
    }
}
