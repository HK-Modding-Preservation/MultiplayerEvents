using MultiplayerEvents.Settings;

namespace MultiplayerEvents
{
    public partial class MultiplayerEvents : IGlobalSettings<GlobalModSettings>, ILocalSettings<LocalModSettings>, ICustomMenuMod
    {

        private string GetVersionSafely()
        {
            return Satchel.AssemblyUtils.GetAssemblyVersionHash();
        }
        public override string GetVersion()
        {
            var version = "Satchel not found";
            try
            {
                version = GetVersionSafely();
            }
            catch (Exception e)
            {
                Modding.Logger.Log(e.ToString());
            }
            return version;
        }

        public static GlobalModSettings GlobalSettings { get; set; } = new GlobalModSettings();
        public static LocalModSettings LocalSettings { get; set; } = new LocalModSettings();
        public void OnLoadGlobal(GlobalModSettings s)
        {
            GlobalSettings = s;
        }

        public GlobalModSettings OnSaveGlobal() => MultiplayerEvents.GlobalSettings;

        public void OnLoadLocal(LocalModSettings s)
        {
            MultiplayerEvents.LocalSettings = s;
        }

        public LocalModSettings OnSaveLocal() => MultiplayerEvents.LocalSettings;
        public bool ToggleButtonInsideMenu { get; } = false;
        public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? toggle)
        {
            return BetterMenu.GetMenu(modListMenu);
        }

    }
}
