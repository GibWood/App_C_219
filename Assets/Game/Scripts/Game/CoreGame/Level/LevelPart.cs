using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Game.CoreGame.Player.Items;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Level
{
    public class LevelPart : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _levelRenderer;
        [SerializeField] private Transform _startPlatformPosition;

        [SerializeField] private Transform _topPlatformPosition;
        [SerializeField] private Transform _bottomPlatformPosition;

        [SerializeField] private List<GameObject> _rotateObjects;

        [SerializeField] private float _minDistance;
        [SerializeField] private float _maxDistance;
        [SerializeField] private int _minCountPlatforms;
        [SerializeField] private int _maxCountPlatforms;

        public float Width => _levelRenderer.bounds.size.x;
        public float PositionX => transform.position.x;
        public bool HasActive { get; private set; }
        public bool StartLevel;

        private PlatformsPool _platformsPool;

        private List<Platform> _platforms = new();

        public void SetActiveStatus(bool active)
        {
            HasActive = active;
        }

        public void Setup(PlatformsPool platformsPool, bool generatePlatforms = true)
        {
            _platformsPool = platformsPool;

            if (generatePlatforms)
                GeneratePlatforms();

            RotateBackgroundObjects();
        }

        public void ReleaseAllPlatforms()
        {
            foreach (var platform in _platforms)
                _platformsPool.Release(platform);

            _platforms.Clear();
        }

        private void GeneratePlatforms()
        {
            InitFirstPlatform();
            InitPlatforms();
        }

        private void InitFirstPlatform()
        {
            var firstPlatform = _platformsPool.Get();
            firstPlatform.InitForUse();
            firstPlatform.transform.position = new Vector3(_startPlatformPosition.position.x, GetRandomYPosition());
            _platforms.Add(firstPlatform);
        }

        private void RotateBackgroundObjects()
        {
            int randomValue = Random.Range(0, 100);
            if (randomValue > 50)
                return;

            foreach (var rotateObject in _rotateObjects)
            {
                var transformLocalScale = rotateObject.transform.localScale;
                rotateObject.transform.localScale =
                    new Vector3(-transformLocalScale.x, transformLocalScale.y, transformLocalScale.z);
            }
        }

        private void InitPlatforms()
        {
            float currentDistance = Random.Range(_minDistance, _maxDistance);
            int currentCountPlatforms = Random.Range(_minCountPlatforms, _maxCountPlatforms);
            for (int i = 0; i < currentCountPlatforms; i++)
            {
                var platform = _platformsPool.Get();
                platform.InitForUse();
                var xPosition = _platforms.Last().transform.position.x +
                                currentDistance * _maxCountPlatforms / currentCountPlatforms;
                platform.transform.position = new Vector3(xPosition, GetRandomYPosition());
                _platforms.Add(platform);
            }
        }

        private float GetRandomYPosition() =>
            Random.Range(_bottomPlatformPosition.position.y, _topPlatformPosition.position.y);
    }
}