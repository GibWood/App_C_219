using Game.Scripts.Game.CoreGame.Player.Player;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.BirdsLogic
{
    public class Bird : MonoBehaviour
    {
        private BirdsSpawner _birdsSpawner;
        private HeroPlayer _heroPlayer;

        public bool Captured { get; private set; }

        public void Initialize(BirdsSpawner birdsSpawner, HeroPlayer heroPlayer)
        {
            _birdsSpawner = birdsSpawner;
            _heroPlayer = heroPlayer;
            Captured = false;
        }

        public void OnBecameInvisible()
        {
            if (transform == null || _heroPlayer == null) return;

            if (transform.position.x < _heroPlayer.transform.position.x)
                _birdsSpawner.Release(this);
        }

        public void Capture() =>
            Captured = true;
    }
}