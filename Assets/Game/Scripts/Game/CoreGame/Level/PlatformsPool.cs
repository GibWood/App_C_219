using Game.Scripts.Game.CoreGame.Player.Items;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Scripts.Game.CoreGame.Level
{
    public class PlatformsPool : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Platform _platformPrefab;

        private ObjectPool<Platform> _levelObjectPool;

        public void Initialize()=>
            _levelObjectPool = new ObjectPool<Platform>(CreateLevelPart, GetLevelPart, ReleaseLevelPart);

        public Platform Get() =>
            _levelObjectPool.Get();

        public void Release(Platform platform) =>
            _levelObjectPool.Release(platform);

        private void ReleaseLevelPart(Platform platform) =>
            platform.gameObject.SetActive(false);

        private void GetLevelPart(Platform platform) =>
            platform.gameObject.SetActive(true);

        private Platform CreateLevelPart()
        {
            var levelPart = Instantiate(_platformPrefab, _parent);
            return levelPart;
        }
    }
}