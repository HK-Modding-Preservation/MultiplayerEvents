using HkmpPouch.DataStorage.Counter;

namespace MultiplayerEvents
{
    public  class TeamData{
        internal string Name;

        internal int TeamId;

        internal CounterClient KillCount,Score;

        public TeamData(string Name,int TeamId){
            this.Name = Name;
            this.TeamId = TeamId;
            CreateCounters();
        }

        private void CreateCounters(){
            KillCount = new CounterClient(MultiplayerEvents.Instance.pipe, $"{TeamId}KillCount");
            KillCount.OnUpdate += (s, e) =>
            {
                GUI.UpdateText();
                MultiplayerEvents.Instance.LogDebug($"{Name} Kill Count : {e.Count}");
            };

            Score = new CounterClient(MultiplayerEvents.Instance.pipe, $"{TeamId}score");
            Score.OnUpdate += (s, e) =>
            {
                GUI.UpdateText();
                MultiplayerEvents.Instance.LogDebug($"{Name} score : {e.Count}");
            };
            KillCount.Get();
            Score.Get();
        }

        public void DestroyCounters() {
            Score?.Destroy();
            KillCount?.Destroy();
        }
    }
}