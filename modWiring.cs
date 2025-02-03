namespace MultiplayerEvents
{
    public partial class MultiplayerEvents : Mod, IGlobalSettings<GlobalModSettings>, ICustomMenuMod
    {

        private string getVersionSafely()
        {
            return Satchel.AssemblyUtils.GetAssemblyVersionHash();
        }
        public override string GetVersion()
        {
            var version = "Satchel not found";
            try
            {
                version = getVersionSafely();
            }
            catch (Exception e)
            {
                Modding.Logger.Log(e.ToString());
            }
            return version;
        }

        public static GlobalModSettings Settings { get; set; } = new GlobalModSettings();
        public void OnLoadGlobal(GlobalModSettings s)
        {
            Settings = s;
        }

        public GlobalModSettings OnSaveGlobal()
        {
            return MultiplayerEvents.Settings;
        }

        public bool ToggleButtonInsideMenu { get; } = false;
        public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? toggle)
        {
            return BetterMenu.GetMenu(modListMenu);
        }
    }
}
