namespace CodeBase.Infrastructure.StateMachine
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
    public interface IPayLoadState<PayLoad> : IExitableState
    {
        public void Enter(PayLoad payLoad);
    }
    public interface IExitableState
    {
        public void Exit();
    }
}