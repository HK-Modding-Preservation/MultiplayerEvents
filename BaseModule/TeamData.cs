using HkmpPouch.DataStorage.Counter;

namespace MultiplayerEvents.BaseModule
{
    public class TeamData
    {
        internal string Name;

        internal int TeamId;
        private TeamScoreUI ui;
        internal CounterClient KillCount, Score;
        private PipeClient pipe;
        public TeamData(PipeClient pipe, TeamScoreUI ui, string Name, int TeamId)
        {
            this.pipe = pipe;
            this.Name = Name;
            this.TeamId = TeamId;
            this.ui = ui;
            CreateCounters();
        }
        private void UpdateText()
        {
            ui.UpdateText(this);
        }
        private void CreateCounters()
        {
            KillCount = new CounterClient(pipe, $"{TeamId}KillCount");
            KillCount.OnUpdate += (s, e) =>
            {
                UpdateText();
                MultiplayerEvents.Instance.LogDebug($"{Name} Kill Count : {e.Count}");
            };

            Score = new CounterClient(pipe, $"{TeamId}score");
            Score.OnUpdate += (s, e) =>
            {
                UpdateText();
                MultiplayerEvents.Instance.LogDebug($"{Name} score : {e.Count}");
            };
            KillCount.Get();
            Score.Get();
        }

        public void DestroyCounters()
        {
            Score?.Destroy();
            KillCount?.Destroy();
        }
    }
}