using HkmpPouch.DataStorage.AppendOnlyList;
using MultiplayerEvents.MultiplayerModule;

namespace MultiplayerEvents.BaseModule
{
    public class PersistentSkulls
    {

        internal Dictionary<string, AppendOnlyListClient> SceneDeaths = new();
        internal Dictionary<string, GameObject> CurrentSkulls = new();
        internal GameObject Skull;
        internal AppendOnlyListClient CurrentScene;
        private PipeClient pipe { get => MainPipe.Instance.pipe; }

        public void Init()
        {
            pipe.OnReady += Pipe_OnReady;
        }

        private void Pipe_OnReady(object sender, EventArgs e)
        {
            pipe.ClientApi.ClientManager.ConnectEvent += ClientManager_ConnectEvent;
            pipe.ClientApi.ClientManager.DisconnectEvent += ClientManager_DisconnectEvent;

        }

        private void ClientManager_DisconnectEvent()
        {
            Unhook();
        }

        private void ClientManager_ConnectEvent()
        {
            Hook();
        }

        public void Hook()
        {
            ModHooks.HeroUpdateHook += ModHooks_HeroUpdateHook;
            ModHooks.BeforePlayerDeadHook += ModHooks_BeforePlayerDeadHook;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        public void Unhook()
        {
            ModHooks.HeroUpdateHook -= ModHooks_HeroUpdateHook;
            ModHooks.BeforePlayerDeadHook -= ModHooks_BeforePlayerDeadHook;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
            CurrentScene.OnUpdate -= Dl_OnUpdate;
        }

        public AppendOnlyListClient GetDeathListForScene(string scene)
        {
            AppendOnlyListClient dl;
            if (!SceneDeaths.TryGetValue(scene, out dl))
            {
                dl = new AppendOnlyListClient(pipe, scene);
                SceneDeaths[scene] = dl;
            }
            return dl;
        }
        private void Dl_OnUpdate(object sender, AppendOnlyListUpdateEventArgs e)
        {
            var dataList = e.data;
            foreach (var data in dataList)
            {
                var splitData = data.Split(new char[] { '|' }, 2);
                if (splitData.Length > 0 && Skull != null)
                {
                    if (!CurrentSkulls.TryGetValue(data, out var sk))
                    {
                        var newSkull = UnityEngine.Object.Instantiate(Skull);
                        newSkull.transform.position = new Vector3(float.Parse(splitData[0]), float.Parse(splitData[1]), Skull.transform.position.z);
                        CurrentSkulls[data] = newSkull;
                    }
                }
            }
            CurrentScene.OnUpdate -= Dl_OnUpdate;
        }

        private void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            CurrentSkulls = new();
            CurrentScene = GetDeathListForScene(arg1.name);
            CurrentScene.OnUpdate += Dl_OnUpdate;
            CurrentScene.GetAll();
        }
        private void ModHooks_BeforePlayerDeadHook()
        {
            var x = HeroController.instance.transform.position.x.ToString("0.00", CultureInfo.InvariantCulture);
            var y = HeroController.instance.transform.position.y.ToString("0.00", CultureInfo.InvariantCulture);
            CurrentScene.Add($"{x}|{y}", 600);
        }
        private void ModHooks_HeroUpdateHook()
        {
            AcquireSkull();
        }
        public void AcquireSkull()
        {
            if (Skull == null)
            {
                GameObject heroDeath = HeroController.instance.transform.Find("Hero Death").gameObject;
                PlayMakerFSM heroDeathAnim = heroDeath.LocateMyFSM("Hero Death Anim");
                Skull = heroDeathAnim.GetAction<CreateObject>("Head Left", 0).gameObject.Value;
            }
        }
    }
}