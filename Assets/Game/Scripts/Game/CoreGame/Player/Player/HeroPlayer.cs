using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class HeroPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _explosion;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private HeroPlayerAudioContext _heroPlayerAudioContext;

        [SerializeField] private SpinePlatformerAnimation2D _spine;

        private PlayerData _playerData;
        private CameraShaker _cameraShaker;

        public PlayerData PlayerData => _playerData;
        public HeroPlayerAudioContext HeroPlayerAudioContext => _heroPlayerAudioContext;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;


        public void Initialize(int maxHp, CameraShaker cameraShaker)
        {
            _playerData = new PlayerData(maxHp);
            _cameraShaker = cameraShaker;
            _spine.PlayIdleAnimation();
        }

        public void GetDamage(int value = -1)
        {
            PlayerData.IncreaseHp(value);

            //_spinePlatformerAnimation2D.PlayHitAnimation();

            _cameraShaker.Shake();

            _heroPlayerAudioContext.PlayPlayerHitAudio();

            Debug.Log("Get Damage");
        }
    }
}