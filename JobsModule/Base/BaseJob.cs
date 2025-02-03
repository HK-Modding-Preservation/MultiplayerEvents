using SFCore;
using System.Linq;

namespace MultiplayerEvents.JobsModule.Base
{
    public class JobState
    {
        public List<string> AcquiredAbilities;
        public List<string> EquippedAbilities;
    }

    public abstract class BaseJob : EasyCharm
    {
        public BaseJob()
        {
            GotCharm = true;
        }
        protected override int GetCharmCost() => 1;
        public abstract List<BaseAbilities> GetAbilities();
        public void OnEquipped()
        {
            var abilities = GetAbilities();
            foreach (var ability in abilities)
            {
                ability.Give();
            }
        }
        public void OnUnEquipped()
        {
            var abilities = GetAbilities();
            SetEquippedAbilities(new List<string>());
            foreach (var ability in abilities)
            {
                ability.Take();
            }
        }
        public void SetAcquiredAbilities(List<string> equippedAbilities)
        {
            var abilities = GetAbilities();
            foreach (var ability in abilities)
            {
                if (equippedAbilities.Contains(ability.GetName()))
                {
                    ability.Give();
                }
                else
                {
                    ability.Take();
                }
            }
        }
        public List<string> GetAcquiredAbilities()
        {
            return GetAbilities().FindAll((ability) => ability.Charm.GotCharm).Select((ability) => ability.GetName()).ToList();
        }
        public void SetEquippedAbilities(List<string> equippedAbilities)
        {
            var abilities = GetAbilities();
            foreach (var ability in abilities)
            {
                if (equippedAbilities.Contains(ability.GetName()))
                {
                    ability.Equip();
                }
                else
                {
                    ability.UnEquip();
                }
            }
        }
        public List<string> GetEquippedAbilities()
        {
            return GetAbilities().FindAll((ability) => ability.Charm.IsEquipped).Select((ability) => ability.GetName()).ToList();
        }
        public JobState GetJobState()
        {
            return new JobState
            {
                AcquiredAbilities = GetAcquiredAbilities(),
                EquippedAbilities = GetEquippedAbilities(),
            };
        }
        public void SetJobState(JobState jobState)
        {
            if (this is BaseJob job)
            {
                job.SetAcquiredAbilities(jobState.AcquiredAbilities);
                job.SetEquippedAbilities(jobState.EquippedAbilities);
            }
        }
    }

}
