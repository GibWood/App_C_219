using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class HeroPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _explosion;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private PlatformerMotor2D _motor2D;
        [SerializeField] private HeroPlayerAudioContext _heroPlayerAudioContext;

        [SerializeField] private SpinePlatformerAnimation2D _spinePlatformerAnimation2D;

        private PlayerData _playerData;
        private CameraShaker _cameraShaker;
        
        public PlayerData PlayerData => _playerData;
        public HeroPlayerAudioContext HeroPlayerAudioContext => _heroPlayerAudioContext;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public PlatformerMotor2D Motor2D => _motor2D;
        

        public void Initialize(int maxHp, CameraShaker cameraShaker)
        {
            _playerData = new PlayerData(maxHp);
            _cameraShaker = cameraShaker;
        }

        public void GetDamage(int value = -1)
        {
            PlayerData.IncreaseHp(value);

            _spinePlatformerAnimation2D.PlayHitAnimation();

            _cameraShaker.Shake();

            _heroPlayerAudioContext.PlayPlayerHitAudio();

            Debug.Log("Get Damage");
        }

        public void StartExplosion()
        {
            gameObject.SetActive(false);

            var explosion = Instantiate
            (
                _explosion,
                transform.position,
                Quaternion.identity
            );

            var time = 3f;

            RotateExplosionByPlayerDirection(explosion);

            Destroy(explosion, time);
        }

        private void RotateExplosionByPlayerDirection(GameObject explosion)
        {
            if (_spinePlatformerAnimation2D.SkeletonAnimation.transform.localScale.x < 0)
            {
                explosion.transform.localScale = new Vector3(explosion.transform.localScale.x * (-1),
                    explosion.transform.localScale.y, explosion.transform.localScale.z);
            }
        }
    }
}