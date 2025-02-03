using MagicUI.Core;
using MagicUI.Elements;

namespace MultiplayerEvents.BaseModule
{
    public class TeamScoreUI
    {
        public TextObject mainText;
        private LayoutRoot layout;
        internal void Init()
        {
            if (layout == null)
            {
                layout = new(true, "Persistent layout");
                layout.RenderDebugLayoutBounds = false;
                Setup(layout);
            }
        }

        public void Setup(LayoutRoot layout)
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

        public void UpdateText(TeamData team)
        {
            if (mainText == null) return;
            if (team.TeamId > 0)
            {
                mainText.Text = $"TEAM : {team.Name} | SCORE : {team.Score?.Count} | KILLCOUNT : {team.KillCount?.Count}";
            }
            else
            {
                mainText.Text = $"Select a team to join Multiplayer quests";
            }
        }
    }
}
