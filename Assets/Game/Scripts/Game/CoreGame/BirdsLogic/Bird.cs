using System;
using Game.Scripts.Game.CoreGame.Player.Player;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.BirdsLogic
{
    public class Bird : MonoBehaviour
    {
        private BirdsSpawner _birdsSpawner;
        private HeroPlayer _heroPlayer;
        
        public void Initialize(BirdsSpawner birdsSpawner, HeroPlayer heroPlayer)
        {
            _birdsSpawner = birdsSpawner;
            _heroPlayer = heroPlayer;
        }

        public void OnBecameInvisible()
        {
            if (transform.position.x < _heroPlayer.transform.position.x) 
                _birdsSpawner.Release(this);
        }
    }
}
