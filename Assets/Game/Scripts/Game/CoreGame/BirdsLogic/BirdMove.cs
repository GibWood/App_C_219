using UnityEngine;

namespace Game.Scripts.Game.CoreGame.BirdsLogic
{
    public class BirdMove : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        public bool MoveAway;

        private void Update()
        {
            if(!MoveAway)
                MoveLeft();
            else
                MoveUp();
        }

        private void MoveLeft()
        {
            float horizontalMovement = -_speed * Time.deltaTime;

            transform.Translate(new Vector3(horizontalMovement, 0f, 0f));
        }

        private void MoveUp()
        {
            float horizontalMovement = -_maxSpeed * Time.deltaTime*2;
            float verticalMovement = _maxSpeed * Time.deltaTime*4;

            transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0f));
        }

        public void SetRandomSpeed()
        {
            var newSpeed = Random.Range(_minSpeed, _maxSpeed);
            SetSpeed(newSpeed);
        }

        public void SetSlowSpeed() =>
            SetSpeed(_minSpeed / 2);

        public void SetSpeed(float newSpeed) =>
            _speed = newSpeed;
    }
}