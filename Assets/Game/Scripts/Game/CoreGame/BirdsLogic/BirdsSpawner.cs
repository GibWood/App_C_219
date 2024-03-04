using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game.Scripts.Game.CoreGame.Player.Player;
using UnityEngine;
using Random = UnityEngine.Random;

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

        private readonly List<Bird> _activeBirds = new();

        public List<Bird> Birds => _activeBirds;

        private Tween _groupTween;

        private void Start()
        {
            _birdsPool.Initialize();
            StartSpawnBirdGroup();
        }

        private void OnDestroy()
        {
            _groupTween?.Kill();
        }

        public void Release(Bird bird)
        {
            _activeBirds.Remove(bird);
            _birdsPool.Release(bird);
        }

        private void StartSpawnBirdGroup()
        {
            int birdsCount = Random.Range(_minBirdsInGroup, _maxBirdsInGroup);
            for (int i = 0; i < birdsCount; i++)
                DOVirtual.DelayedCall(_birdSpawnDelay * i, SpawnBird);

            _groupTween = DOVirtual.DelayedCall(_groupSpawnDelay, StartSpawnBirdGroup);
        }

        private void SpawnBird()
        {
            var bird = _birdsPool.Get();

            if (bird == null) return;

            bird.transform.position = new Vector3(_spawnPosition.position.x,
                _spawnPosition.position.y + Random.Range(-0.8f, 0.5f));
            bird.Initialize(this, _heroPlayer);
            var birdMove = bird.GetComponent<BirdMove>();
            birdMove.MoveAway = false;
            birdMove.SetRandomSpeed();

            _activeBirds.Add(bird);
        }
    }
}