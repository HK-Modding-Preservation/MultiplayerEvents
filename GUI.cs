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

        public static string teamName()
        {

            if (team == "0")
            {
                return "None";
            } else if (team == "1")
            {
                return "Moss";
            }
            else if (team == "2")
            {
                return "Hive";
            }
            else if (team == "3")
            {
                return "Grimm";
            }
            else if (team == "4")
            {
                return "Lifeblood";
            }

            return "None";
        }
        public static void UpdateText()
        {
            if (mainText == null) return;
            mainText.Text = $"TEAM : {teamName()} | SCORE : {MultiplayerEvents.Instance.score?.Count} | KILLCOUNT : {MultiplayerEvents.Instance.killCount?.Count}";
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
    }
}
