using UnityEngine;

namespace Game.Scripts.Game.CoreGame.BirdsLogic
{
    public class BirdMove : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private void Update()
        {
            float horizontalMovement = -_speed * Time.deltaTime; 

            transform.Translate(new Vector3(horizontalMovement, 0f, 0f));
        }

        public void SetRandomSpeed()
        {
            var newSpeed = Random.Range(_minSpeed, _maxSpeed);
            SetSpeed(newSpeed);
        }

        public void SetSpeed(float newSpeed) => 
            _speed = newSpeed;
    }
}