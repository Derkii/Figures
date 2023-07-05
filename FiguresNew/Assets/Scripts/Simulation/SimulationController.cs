using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simulation
{
    public class SimulationController : MonoBehaviour, ISimulationController
    {
        private IEnumerable<INextFrameSimulation> _nextFrameSimulations;
        private bool _stopped;

        private void Awake()
        {
            Finder.AddComponent(this);
        }

        private void Start()
        {
            var startSimulate = Finder.FindByInterfaces(typeof(IStartSimulation)).Select(t => t as IStartSimulation);
            foreach (var simulate in startSimulate)
            {
                simulate.OnSimulationStarted();
            }
            
            _nextFrameSimulations = Finder.FindByInterfaces(typeof(INextFrameSimulation))
                .Select(t => t as INextFrameSimulation);
        }

        private void Update()
        {
            if (_stopped) return;

            foreach (var nextFrameSimulation in _nextFrameSimulations)
            {
                nextFrameSimulation.NextFrame();
            }
        }

        public void Stop(bool stop)
        {
            _stopped = stop;
            foreach (var stopSimulation in Finder.FindByInterfaces(typeof(IStopSimulation))
                         .Select(t => t as IStopSimulation))
            {
                stopSimulation.OnSimulationStopped(stop);
            }
        }
    }
}