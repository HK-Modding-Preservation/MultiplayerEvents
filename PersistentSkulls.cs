namespace MultiplayerEvents{
    public static class PersistentSkulls{

        internal static Dictionary<string, AppendOnlyList> SceneDeaths = new();
        internal static Dictionary<string, GameObject> CurrentSkulls = new();
        internal static GameObject Skull;
        internal static AppendOnlyList CurrentScene;
        static PersistentSkulls(){
            ModHooks.HeroUpdateHook += ModHooks_HeroUpdateHook;
            ModHooks.BeforePlayerDeadHook += ModHooks_BeforePlayerDeadHook;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }
        
        public static AppendOnlyList GetDeathListForScene(string scene) {
            AppendOnlyList dl;
            if (!SceneDeaths.TryGetValue(scene, out dl)) { 
                dl = new AppendOnlyList(MultiplayerEvents.Instance.pipe,scene);
                SceneDeaths[scene] = dl;
            }
            return dl;
        }
        private static void Dl_OnUpdate(object sender, AppendOnlyListUpdateEventArgs e)
        {
            var dataList = e.data;
            foreach(var data in dataList)
            {
                var splitData = data.Split( new char[] { '|' } , 2);
                if(splitData.Length > 0 && Skull != null)
                {
                    if(!CurrentSkulls.TryGetValue(data, out var sk))
                    {
                        var newSkull = GameObject.Instantiate(Skull);
                        newSkull.transform.position = new Vector3(float.Parse(splitData[0]), float.Parse(splitData[1]), Skull.transform.position.z);
                        CurrentSkulls[data] = newSkull;
                    }
                }
            }
            CurrentScene.OnUpdate -= Dl_OnUpdate;
        }

        private static void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            CurrentSkulls = new();
            CurrentScene = GetDeathListForScene(arg1.name);
            CurrentScene.OnUpdate += Dl_OnUpdate;
            CurrentScene.GetAll();
        }
        private static void ModHooks_BeforePlayerDeadHook()
        {
            var x = HeroController.instance.transform.position.x.ToString("0.00", CultureInfo.InvariantCulture);
            var y = HeroController.instance.transform.position.y.ToString("0.00", CultureInfo.InvariantCulture);
            CurrentScene.Add($"{x}|{y}", 600);
        }
        private static void ModHooks_HeroUpdateHook()
        {
            AcquireSkull();
        }
        public static void AcquireSkull(){
            if(Skull == null)
            {
                GameObject heroDeath = HeroController.instance.transform.Find("Hero Death").gameObject;
                PlayMakerFSM heroDeathAnim = heroDeath.LocateMyFSM("Hero Death Anim");
                Skull = heroDeathAnim.GetAction<CreateObject>("Head Left", 0).gameObject.Value;
            }
        }
    }
}