using System;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class HeroMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _distanceToEndMove;
        [SerializeField] private float _forceEndMove = 5f;

        private bool _canMove;
        private Transform _target;
        private Vector3 _direction;

        public event Action OnEndMove;

        private void LateUpdate() =>
            Move(_target);

        public void MoveHero(Transform target)
        {
            _rigidbody2D.velocity = Vector2.zero;

            _target = target;
            _canMove = true;

            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            _direction = (target.position - transform.position).normalized;
        }

        public void StopMove()
        {
            _target = null;
            _canMove = false;
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        private void Move(Transform target)
        {
            if (target == null)
                return;

            if (!_canMove) return;

            transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);

            float distance = Mathf.Abs(Vector3.Distance(transform.position, target.position));
            if (distance <= _distanceToEndMove)
            {
                OnEndMove?.Invoke();

                _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody2D.AddForce(_direction * _forceEndMove, ForceMode2D.Impulse);
                Debug.Log("End move");
            }
        }
    }
}