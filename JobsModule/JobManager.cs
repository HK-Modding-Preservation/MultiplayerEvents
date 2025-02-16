using MultiplayerEvents.JobsModule.Abilities.MinorHeal;
using MultiplayerEvents.JobsModule.Base;
using MultiplayerEvents.MultiplayerModule;

namespace MultiplayerEvents.JobsModule
{
    public class JobManager
    {

        public List<BaseJob> AvailableJobs = new List<BaseJob>();

        public List<BaseAbilities> AvailableAbilities;
        private PipeClient pipe { get => MainPipe.Instance.pipe; }

        internal void Init()
        {
            /*
            AvailableJobs.Add(new Healer());
            AvailableJobs.Add(new Charger());
            AvailableJobs.Add(new Assassin());
            */
            AvailableAbilities = new List<BaseAbilities>()
            {
                new MinorHeal()
            };
            foreach(BaseAbilities ability in AvailableAbilities)
            {
                ability.Give();
            }
            On.HeroController.CharmUpdate += HeroController_CharmUpdate;
            pipe.OnRecieve += Pipe_OnRecieve;
        }

        private void Pipe_OnRecieve(object sender, ReceivedEventArgs e)
        {
            if (e.Data.EventName[0] == 'C')
            {
                var identifiers = e.Data.EventName.Split('|');
                switch (identifiers[1])
                {
                    case "PoisonDebuff":
                        //create new object with identifiers[2] & e.Data.EventData
                        break;
                    default:
                        break;
                }
            }
        }

        private void HeroController_CharmUpdate(On.HeroController.orig_CharmUpdate orig, HeroController self)
        {
            /*if (assassin.IsEquipped)
            {
                charger.TakeCharm();
                healer.GiveCharm();
            }
            else if (healer.IsEquipped)
            {
                charger.GiveCharm();
                assassin.TakeCharm();
            }
            else if (charger.IsEquipped)
            {
                healer.TakeCharm();
                assassin.GiveCharm();
            }*/
            orig(self);
        }

    }
}
