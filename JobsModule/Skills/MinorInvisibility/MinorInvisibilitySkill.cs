using MultiplayerEvents.JobsModule.Base.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerEvents.JobsModule.Skills.MinorInvisibility
{
    public class MinorInvisibilitySkill : FocusSkill
    {
        public override string AbilityId => "MinorInvisibilitySkill";

        public override int GetCost() => 1;

        public override string GetDescription() => FlavorTexts.AssassinDescription;

        public override Sprite GetIcon() => AssetManager.AssassinSprite;

        public override void OnTriggerLocal()
        {
            pipe.Broadcast(AbilityId, "", true, true);
            HeroController.instance.gameObject.GetComponent<Renderer>().enabled = false;
            CoroutineHelper.WaitForSecondsBeforeInvoke(10f, () =>
            {
            HeroController.instance.gameObject.GetComponent<Renderer>().enabled = true;
            });
        }

        public override void OnTriggerRemote(EventContainer data)
        {
            var player = pipe.ClientApi.ClientManager.GetPlayer(data.FromPlayer);
            player.PlayerContainer.SetActive(false);
            CoroutineHelper.WaitForSecondsBeforeInvoke(10f, () =>
            {
                player.PlayerContainer.SetActive(true);
            });
        }
    }
}
