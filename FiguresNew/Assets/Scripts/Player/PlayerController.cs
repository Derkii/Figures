using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Simulation;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IStartSimulation, INextFrameSimulation
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Path _path;
        [SerializeField] private LineRenderer _lineRenderer;
        private TweenerCore<Vector3, Vector3, VectorOptions> _currentTween;
        private int _currentPointIndex;

        private CancellationTokenSource _cancellationToken;

        private void OnDisable()
        {
            _cancellationToken.Cancel();
        }

        private void OnEnable()
        {
            if (_cancellationToken != null)
                StartMove();
        }

        private void OnDestroy()
        {
            _cancellationToken.Cancel();
        }

        public void OnSimulationStarted()
        {
            Application.targetFrameRate = 300;
            QualitySettings.vSyncCount = 0;
            _cancellationToken = new CancellationTokenSource();
            StartMove();
        }

        private async UniTask Move(Vector3[] path)
        {
            transform.position = _path.GetPath().ElementAt(0);
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, transform.position);


            for (int i = 1; i < path.Length; i++)
            {
                var startPosition = transform.position;
                var endValue = path[i];
                _currentTween = transform.DOMove(endValue,
                    (endValue - startPosition).magnitude / _speed);
                _currentTween.SetUpdate(UpdateType.Manual);
                _currentTween.OnUpdate(() =>
                {
                    _lineRenderer.positionCount++;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, transform.position);
                });
                await _currentTween.AsyncWaitForCompletion();
            }

            _lineRenderer.positionCount = 0;
        }

        private async UniTaskVoid StartMove()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                await Move(_path.GetPath().ToArray());
                _lineRenderer.SetPositions(Array.Empty<Vector3>());
            }
        }

        public void NextFrame()
        {
            if (this.enabled == false) return;
            _currentTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}