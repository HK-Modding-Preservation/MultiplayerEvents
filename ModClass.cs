using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;
using HkmpPouch;
using HkmpPouch.PouchDataClient;
using MagicUI.Core;
using HutongGames.PlayMaker.Actions;
using static Satchel.FsmUtil;
using System.Globalization;
using Satchel;
using static Satchel.GameObjectUtils;

namespace MultiplayerEvents
{
    public class MultiplayerEvents : Mod
    {
        internal static MultiplayerEvents Instance;

        internal HkmpPipe pipe = new HkmpPipe("MultiplayerEvents",false);

        internal Counter killCount,score;

        internal Dictionary<string, AppendOnlyList> sceneDeaths = new();

        internal Dictionary<string, GameObject> currentSkulls = new();

        internal AppendOnlyList currentSene;

        private LayoutRoot layout;

        private GameObject skull;

        public AppendOnlyList GetDeathListForScene(string scene) {
            AppendOnlyList dl;
            if (!sceneDeaths.TryGetValue(scene, out dl)) { 
                dl = new AppendOnlyList(pipe,scene);
                sceneDeaths[scene] = dl;
            }
            return dl;
        }

        public void CreateCountersForTeam(string team) {
            GUI.team = team;
            killCount = new Counter(pipe, $"{team}KillCount");
            killCount.OnUpdate += (s, e) =>
            {
                GUI.UpdateText();
                LogDebug($"Current Kill Count : {e.Count}");
            };

            score = new Counter(pipe, $"{team}score");
            score.OnUpdate += (s, e) =>
            {
                GUI.UpdateText();
                LogDebug($"Current score : {e.Count}");
            };
            killCount.Get();
            score.Get();
        }

        public void DestroyCounters() {
            score?.Destroy();
            killCount?.Destroy();
        }
        public override void Initialize()
        {
            Instance = this;
     
            ModHooks.OnReceiveDeathEventHook += ModHooks_OnReceiveDeathEventHook;
            ModHooks.BeforePlayerDeadHook += ModHooks_BeforePlayerDeadHook;
            ModHooks.AfterPlayerDeadHook += ModHooks_AfterPlayerDeadHook;
            ModHooks.HeroUpdateHook += ModHooks_HeroUpdateHook;
            On.HeroController.Awake += HeroController_Awake;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void ModHooks_BeforePlayerDeadHook()
        {
            var x = HeroController.instance.transform.position.x.ToString("0.00", CultureInfo.InvariantCulture);
            var y = HeroController.instance.transform.position.y.ToString("0.00", CultureInfo.InvariantCulture);
            currentSene.Add($"{x}|{y}", 600);
        }

        private void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            currentSkulls = new();
            currentSene = GetDeathListForScene(arg1.name);
            currentSene.OnUpdate += Dl_OnUpdate;
            currentSene.GetAll();
        }

        private void Dl_OnUpdate(object sender, AppendOnlyListUpdateEventArgs e)
        {
            var dataList = e.data;
            foreach(var data in dataList)
            {
                var splitData = data.Split( new char[] { '|' } , 2);
                if(splitData.Length > 0 && skull != null)
                {
                    if(!currentSkulls.TryGetValue(data, out var sk))
                    {
                        var newSkull = GameObject.Instantiate(skull);
                        newSkull.transform.position = new Vector3(float.Parse(splitData[0]), float.Parse(splitData[1]), skull.transform.position.z);
                        currentSkulls[data] = newSkull;
                    }
                }
            }
            currentSene.OnUpdate -= Dl_OnUpdate;
        }

        private void ModHooks_HeroUpdateHook()
        {
            ListenToClientEvents();
            if(skull == null)
            {
                GameObject heroDeath = HeroController.instance.transform.Find("Hero Death").gameObject;
                PlayMakerFSM heroDeathAnim = heroDeath.LocateMyFSM("Hero Death Anim");
                skull = heroDeathAnim.GetAction<CreateObject>("Head Left", 0).gameObject.Value;
            }
            if (HkmpPouch.Client.Instance.clientApi != null && lastTeam != (int)HkmpPouch.Client.Instance.clientApi.ClientManager.Team)
            {
                var team = ((int)HkmpPouch.Client.Instance.clientApi.ClientManager.Team);
                DestroyCounters();
                CreateCountersForTeam(team.ToString());
                lastTeam = team;
            }
        }

        private bool listening = false;

        private int lastTeam = -1;
        private void ListenToClientEvents()
        {
            if (listening) { return;  }
            if (HkmpPouch.Client.Instance.clientApi == null) { return; }
            HkmpPouch.Client.Instance.clientApi.ClientManager.ConnectEvent += ClientManager_ConnectEvent;
            HkmpPouch.Client.Instance.clientApi.ClientManager.DisconnectEvent += ClientManager_DisconnectEvent;
            listening = true;
        }

        private void ClientManager_DisconnectEvent()
        {

            LogDebug("disconnect");
            DestroyCounters();
        }

        private void ClientManager_ConnectEvent()
        {

            LogDebug("connect");
            score.Get();
            killCount.Get();
        }

        private void HeroController_Awake(On.HeroController.orig_Awake orig, HeroController self)
        {
            if (layout == null)
            {
                layout = new(true, "Persistent layout");
                layout.RenderDebugLayoutBounds = false;
                GUI.Setup(layout);
            }

            orig(self);

        }

        private void ModHooks_AfterPlayerDeadHook()
        {
            score.Decrement(5);
        }

        private void ModHooks_OnReceiveDeathEventHook(EnemyDeathEffects enemyDeathEffects, bool eventAlreadyReceived, ref float? attackDirection, ref bool resetDeathEvent, ref bool spellBurn, ref bool isWatery)
        {
            if(eventAlreadyReceived) { return; }
            killCount.Increment(1);
            ushort points = 1;
            var enemyName = enemyDeathEffects.gameObject.GetName(true);
            foreach (var enemy in EnemyDatabase.hardEnemyTypeNames)
            {
                if (enemy.ToLower() == enemyName.ToLower())
                {
                    points += 4;
                }
            }
            foreach (var enemy in EnemyDatabase.bigEnemyTypeNames)
            {
                if(enemy.ToLower() == enemyName.ToLower())
                {
                    points += 1;
                }
            }
            score.Increment(points);
        }
    }
}