using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        [SerializeField] private float _offset = 5f;

        private Vector3 _lastPosition;
        
        private bool _canFollow;

        private void Start()
        {
            EnableFollow(true);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void EnableFollow(bool enable)
        {
            _canFollow = enable;
        }

        public void SetTargetPosition(Vector3 target)
        {
            _canFollow = false;
            transform.position = new Vector3(target.x - _offset, transform.position.y,
                transform.position.z);
            _canFollow = true;
        }

        private void LateUpdate()
        {
            if (_target == null || !_canFollow) return;

            if (_target.transform.position.x < -_offset) return;

            if (_target.transform.position.x + _offset < transform.position.x) return;

            Vector3 newPos = new Vector3(_target.transform.position.x + _offset, transform.position.y,
                transform.position.z);
            transform.position = newPos;
        }
    }
}