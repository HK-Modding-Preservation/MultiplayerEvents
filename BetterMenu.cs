namespace MultiplayerEvents
{
    internal static class BetterMenu
    {

        internal static GlobalModSettings defaultSettings = new GlobalModSettings();
        internal static Menu MenuRef;
        internal static Menu PrepareMenu()
        {
            return new Menu("Multiplayer Events", new Element[]{
                new HorizontalOption(
                    "DeathLink", "Links your life to others",
                    new string[] { "Disabled", "Enabled" },
                    (setting) => { MultiplayerEvents.Settings.DeathLinkEnabled = (setting == 1); },
                    () => MultiplayerEvents.Settings.DeathLinkEnabled ? 1 : 0,
                    Id:"DeathLink"),
                 new HorizontalOption(
                    "DeathLink Room?", "Links your life only to others in the same room",
                    new string[] { "Disabled", "Enabled" },
                    (setting) => { MultiplayerEvents.Settings.DeathLinkRoom = (setting == 1); },
                    () => MultiplayerEvents.Settings.DeathLinkRoom ? 1 : 0,
                    Id:"DeathLink"),
                 new HorizontalOption(
                    "DeathLink Team?", "Links your life only to others in the same team",
                    new string[] { "Disabled", "Enabled" },
                    (setting) => { MultiplayerEvents.Settings.DeathLinkTeam = (setting == 1); },
                    () => MultiplayerEvents.Settings.DeathLinkTeam ? 1 : 0,
                    Id:"DeathLink")
            });
        }
        internal static MenuScreen GetMenu(MenuScreen lastMenu)
        {
            if (MenuRef == null)
            {
                MenuRef = PrepareMenu();
            }
            return MenuRef.GetCachedMenuScreen(lastMenu);
        }
    }
}
