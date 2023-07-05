namespace Simulation
{
    public interface IStartSimulation
    {
        public void OnSimulationStarted();
    }

    public interface INextFrameSimulation
    {
        public void NextFrame();
    }

    public interface IStopSimulation
    {
        public void OnSimulationStopped(bool stop);
    }
    public interface ISimulationController
    {
        public void Stop(bool stop);
    }
}