using System;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class HookLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _startLinePoint;
        [SerializeField] private float _minDistanceToCaptured;
        [SerializeField] private float _moveSpeed;

        private Vector3 _targetPoint;
        private bool _canDraw;

        private bool _canMove;
        private Transform _target;

        private bool _captured;
        private Vector3 _currentPosition;

        public event Action OnCaptured;

        private void Update()
        {
            DrawLine();
            MoveLine(_target);
        }

        public void EnableLineRenderer(bool enable)
        {
            _currentPosition = _startLinePoint.position;
            _lineRenderer.gameObject.SetActive(enable);
            _canDraw = enable;

            EnableCaptureStatus(false);
        }

        public void EnableCaptureStatus(bool enable) =>
            _captured = enable;

        public void MoveLine(bool enable, Transform target = null)
        {
            _target = target;
            _canMove = enable;
        }

        public void SetTargetPoint(Vector3 target) =>
            _targetPoint = target;

        private void MoveLine(Transform target)
        {
            if (target == null)
                return;

            if (!_canMove) return;

            if (!_captured)
            {
                _currentPosition = Vector3.MoveTowards(_currentPosition, target.position, _moveSpeed * Time.deltaTime);

                float distance = Mathf.Abs(Vector3.Distance(_currentPosition, target.position));
                if (distance <= _minDistanceToCaptured)
                {
                    _captured = true;
                    OnCaptured?.Invoke();
                    Debug.Log("Capture");
                }
            }
            else
                _currentPosition = target.position;

            SetTargetPoint(_currentPosition);
        }

        private void DrawLine()
        {
            if (!_canDraw)
                return;

            _lineRenderer.SetPosition(0, GetLocalStartPosition());
            _lineRenderer.SetPosition(1, _targetPoint);
        }

        private Vector3 GetLocalStartPosition() =>
            _startLinePoint.position;
    }
}