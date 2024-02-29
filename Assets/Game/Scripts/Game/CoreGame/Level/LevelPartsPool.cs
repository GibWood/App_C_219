using UnityEngine;
using UnityEngine.Pool;

namespace Game.Scripts.Game.CoreGame.Level
{
    public class LevelPartsPool : MonoBehaviour
    {
        [SerializeField] private Transform _levelParent;
        private LevelPart _levelPartPrefab;

        private ObjectPool<LevelPart> _levelObjectPool;

        public void Initialize(LevelPart levelPartPrefab)
        {
            _levelPartPrefab = levelPartPrefab;
            _levelObjectPool = new ObjectPool<LevelPart>(CreateLevelPart, GetLevelPart, ReleaseLevelPart);
        }

        public LevelPart Get() => 
            _levelObjectPool.Get();

        public void Release(LevelPart level) =>
            _levelObjectPool.Release(level);

        private void ReleaseLevelPart(LevelPart levelPart) =>
            levelPart.gameObject.SetActive(false);

        private void GetLevelPart(LevelPart levelPart) =>
            levelPart.gameObject.SetActive(true);

        private LevelPart CreateLevelPart()
        {
            var levelPart = Instantiate(_levelPartPrefab, _levelParent);
            return levelPart;
        }
    }
}