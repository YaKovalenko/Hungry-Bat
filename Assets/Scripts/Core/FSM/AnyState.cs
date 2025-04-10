namespace Orchid.FSM
{
    public sealed class AnyState : State
    {
        public bool AllowLoopEnter { get; } = false;
        public State Breadcrumb { get; set; }
    }
}