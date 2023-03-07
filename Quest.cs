namespace MultiplayerEvents
{
    public abstract class CounterQuest{
        public string Name = "Quest_Name";
        public int Count = 0;
        public int MaxValue = 1;
        public bool Enabled = false;
        public void Enable(){
            if(Enabled){ return;}
            Enabled = true;
            OnEnable();
        }
        public void Disable(){
            if(!Enabled){ return;}
            Enabled = false;
            OnDisable();
        }

        public virtual void OnEnable(){}
        public virtual void OnDisable(){}
        
    }

    public class EnemyKillQuest : CounterQuest{
        public List<string> EnemyName;
        public EnemyKillQuest(string Name,List<string> EnemyName,int MaxValue): base(){
            this.Name = Name;
            this.EnemyName = EnemyName;
            this.MaxValue = MaxValue;
        }
        public override void OnEnable(){
            Chat.AddMessage($"Acquired Quest : {Name}");
            ModHooks.OnReceiveDeathEventHook += ModHooks_OnReceiveDeathEventHook;
        }
        public override void OnDisable(){
            ModHooks.OnReceiveDeathEventHook -= ModHooks_OnReceiveDeathEventHook;
        }
        private void ModHooks_OnReceiveDeathEventHook(EnemyDeathEffects enemyDeathEffects, bool eventAlreadyReceived, ref float? attackDirection, ref bool resetDeathEvent, ref bool spellBurn, ref bool isWatery)
        {
            if(eventAlreadyReceived) { return; }
            var enemyName = enemyDeathEffects.gameObject.GetName(true);
            if(EnemyName.Contains(enemyName)){
                this.Count++;
            }
            if(Count >= MaxValue){
                Disable();
                Chat.AddMessage($"Completed Quest : {Name}");
            }
        }
    }
    public static class QuestManager{
        public static List<CounterQuest> CounterQuests = new();
        public static Dictionary<string,List<string>> EnemyNames;
        public static Dictionary<string,string> internalToExternalNames = new();
        public static System.Random rnd = new System.Random();
        public static void loadEnemyMaps(){
           if(EnemyNames != null) { return;}
           var enemyNames = System.Text.Encoding.Default.GetString(AssemblyUtils.GetBytesFromResources("EnemyNameMap.json"));
           EnemyNames = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,List<string>>>(enemyNames);
           foreach(var kvp in EnemyNames){
               foreach(var v in kvp.Value){
                internalToExternalNames[v] = kvp.Key;
               }
           }
        }
        public static void createNewQuests(){
            loadEnemyMaps();
            var enemyList = new List<string>(EnemyNames.Keys);
            var i = rnd.Next(enemyList.Count);
            var enemy = enemyList[i];
            var count = 5;
            var quest = new EnemyKillQuest($"Defeat {count} {enemy}",EnemyNames[enemy],count);
            quest.Enable();
        }
    }
}