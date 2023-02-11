using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicUI.Core;
using MagicUI.Elements;

namespace MultiplayerEvents
{
    public static class GUI
    {
        public static TextObject mainText;
        public static string team;
        private static LayoutRoot layout;
        public static void Init(){
            On.HeroController.Awake += HeroController_Awake;
        }
        private static void HeroController_Awake(On.HeroController.orig_Awake orig, HeroController self)
        {
            if (layout == null)
            {
                layout = new(true, "Persistent layout");
                layout.RenderDebugLayoutBounds = false;
                Setup(layout);
            }

            orig(self);

        }
        public static void Setup(LayoutRoot layout)
        {

            mainText = new TextObject(layout)
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                FontSize = 20,
                Font = UI.TrajanBold,
                Text = "",
                Padding = new Padding(0, 0, 15, 15)
            };

        }
        
        public static void UpdateText()
        {
            if (mainText == null) return;
            var team = MultiplayerEvents.Instance.CurrentTeam;
            if(team.TeamId > 0){
                mainText.Text = $"TEAM : {team.Name} | SCORE : {team.Score?.Count} | KILLCOUNT : {team.KillCount?.Count}";
            } else {
                mainText.Text = $"Select a team to join Multiplayer quests";
            }
        }
    }
}
