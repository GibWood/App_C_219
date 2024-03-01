using DG.Tweening;
using Game.Scripts.Game.CoreGame.Player.Player;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Items
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GoldItem _goldItem;
        [SerializeField] private int _chanceDropItem = 10;

        [SerializeField] private float _shakeDuration = 0.5f;
        [SerializeField] private float _shakeStrength = 0.05f; 
        [SerializeField] private int _vibrato = 5; 
        [SerializeField] private float _randomness = 45f;

        public bool _triggered;

        public void InitForUse()
        {
            _triggered = false;
            ActivateGoldItem();
        }

        private void ActivateGoldItem()
        {
            _goldItem.gameObject.SetActive(false);
            int chance = Random.Range(0, 100);

            if (chance <= _chanceDropItem)
                _goldItem.gameObject.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<HeroPlayer>() == null || _triggered) return;

            other.GetComponent<HeroPlayer>().PlayerData.IncreaseGolds(1);
            ShakePlatform();
            _triggered = true;
        }

        private void ShakePlatform()
        {
            transform.DOShakeRotation(_shakeDuration, new Vector3(0f, 0f, _shakeStrength),
                _vibrato, _randomness);
        }
    }
}