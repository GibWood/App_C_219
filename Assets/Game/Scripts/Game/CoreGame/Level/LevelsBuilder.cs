using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Game.CoreGame.Player.Player;
using Game.Scripts.Game.ShopLogic;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Level
{
    public class LevelsBuilder : MonoBehaviour
    {
        [SerializeField] private HeroPlayer _player;

        [SerializeField] private LevelPartsPool _levelPartsPool;
        [SerializeField] private PlatformsPool _platformsPool;
        [SerializeField] private ShopElementContextData _shopData;
        [SerializeField] private LevelPart _startLevelPart;

        private List<LevelPart> _levelParts = new List<LevelPart>();
        private int _maxLevelPartsCount = 3;

        private bool _canBuild;

        public bool CanBuild => _canBuild;

        private void Start()
        {
            _platformsPool.Initialize();
            _startLevelPart.Setup(_platformsPool, false);
            _levelPartsPool.Initialize(_shopData.GetCurrentElement().LevelPrefab);
            _levelParts.Add(_startLevelPart);

            EnableBuild(true);
        }

        private void Update()
        {
            if (!_canBuild)
            {
                return;
            }

            float playerPositionX = _player.transform.position.x;

            LevelPart frontLevel = _levelParts.First();

            if (!(frontLevel.PositionX - playerPositionX < frontLevel.Width / 4f)) return;

            InitNextLevelPart(frontLevel);

            if (_levelParts.Count <= _maxLevelPartsCount) return;

            LevelPart lastLevelPart = _levelParts.Last();

            _levelParts.Remove(lastLevelPart);

            ReleaseLevelPart(lastLevelPart);
        }

        private void EnableBuild(bool enable)
        {
            _canBuild = enable;
        }

        private void InitNextLevelPart(LevelPart frontLevel)
        {
            float nextLevelPositionX = frontLevel.PositionX + frontLevel.Width;

            LevelPart nextLevelPart = _levelPartsPool.Get();
            nextLevelPart.transform.position = new Vector3(nextLevelPositionX,
                _startLevelPart.transform.position.y, _startLevelPart.transform.position.z);
            nextLevelPart.SetActiveStatus(true);

            nextLevelPart.Setup(_platformsPool);

            _levelParts.Insert(0, nextLevelPart);
        }

        private void ReleaseLevelPart(LevelPart levelPart)
        {
            levelPart.ReleaseAllPlatforms();
            _levelPartsPool.Release(levelPart);
            levelPart.SetActiveStatus(false);
        }
    }
}