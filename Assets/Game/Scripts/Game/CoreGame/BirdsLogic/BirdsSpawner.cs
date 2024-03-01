using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game.Scripts.Game.CoreGame.Player.Player;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.BirdsLogic
{
    public class BirdsSpawner : MonoBehaviour
    {
        [SerializeField] private HeroPlayer _heroPlayer;
        [SerializeField] private BirdsPool _birdsPool;
        [SerializeField] private Transform _spawnPosition;

        [SerializeField] private float _groupSpawnDelay = 2f;
        [SerializeField] private int _minBirdsInGroup = 3;
        [SerializeField] private int _maxBirdsInGroup = 5;
        [SerializeField] private float _birdSpawnDelay = 0.5f;
        
        private List<Bird> _activeBirds;

        public List<Bird> Birds => _activeBirds;

        private void Start()
        {
            _birdsPool.Initialize();
            StartSpawnBirdGroup();
        }

        public void Release(Bird bird)
        {
            _activeBirds.Remove(bird);
            _birdsPool.Release(bird);
        }

        public Bird NearestBird()
        {
            if (_activeBirds.Count == 0)
                return null;

            var rightBirds = _activeBirds.Where(bird => bird.transform.position.x > _heroPlayer.transform.position.x);
            
            if (rightBirds.Count() == 0)
                return null;

            var nearestBird = rightBirds.OrderBy(bird => Vector3.Distance(bird.transform.position, _heroPlayer.transform.position)).First();
            return nearestBird;
        }

        private void StartSpawnBirdGroup()
        {
            int birdsCount = Random.Range(_minBirdsInGroup, _maxBirdsInGroup);
            for (int i = 0; i < birdsCount; i++)
                DOVirtual.DelayedCall(_birdSpawnDelay * i, SpawnBird);

            DOVirtual.DelayedCall(_groupSpawnDelay, StartSpawnBirdGroup);
        }

        private void SpawnBird()
        {
            var bird = _birdsPool.Get();
            bird.transform.position = new Vector3(_spawnPosition.position.x,
                _spawnPosition.position.y + Random.Range(-0.5f, 0.5f));
            bird.Initialize(this, _heroPlayer);
            bird.GetComponent<BirdMove>().SetRandomSpeed();
            
            _activeBirds.Add(bird);
        }
    }
}