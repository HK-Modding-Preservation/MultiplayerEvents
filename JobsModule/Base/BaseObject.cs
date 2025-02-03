namespace MultiplayerEvents.JobsModule.Base
{
    public class ReceivedEventArgs<State> : EventArgs
    {
        public State Data { get; set; }
    }
    public abstract class BaseObject<State> : MonoBehaviour
    {
        public abstract string GetObjectName();
        public abstract bool SameScene { get; protected set; }
        protected abstract string SerializeState();
        protected abstract State DeserializeState(string state);
        public State GetState() => _state;
        public void SetState(State state)
        {
            _state = state;
            var currentState = SerializeState();
            if (_lastState != currentState)
            {
                UpdateRemote(currentState);
            }
            _lastState = currentState;
        }

        public BaseObject(PipeClient pipe)
        {
            _pipe = pipe;
            _pipe.OnRecieve += _pipe_OnRecieve;
        }
        private void _pipe_OnRecieve(object sender, ReceivedEventArgs e)
        {
            if (e.Data.EventName == "U" + GetObjectName())
            {
                _state = DeserializeState(e.Data.EventData);
            }
            OnRecieve?.Invoke(sender, new ReceivedEventArgs<State> { Data = _state });
        }

        public event EventHandler<ReceivedEventArgs<State>> OnRecieve;
        private string _lastState;
        private State _state;
        private PipeClient _pipe;
        private void UpdateRemote(String currentState)
        {
            _pipe.Broadcast("U" + GetObjectName(), currentState, SameScene, true);
        }
    }
}
