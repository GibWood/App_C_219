using UnityEngine;
using UnityEngine.Pool;

namespace Game.Scripts.Game.CoreGame.BirdsLogic
{
    public class BirdsPool : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Bird _birdPrefab;

        private ObjectPool<Bird> _birdsPool;

        public void Initialize() =>
            _birdsPool = new ObjectPool<Bird>(CreateBird, GetBird, ReleaseBird);

        public Bird Get() =>
            _birdsPool.Get();

        public void Release(Bird bird) =>
            _birdsPool.Release(bird);

        private void ReleaseBird(Bird bird) =>
            bird.gameObject.SetActive(false);

        private void GetBird(Bird platform) =>
            platform.gameObject.SetActive(true);

        private Bird CreateBird()
        {
            var bird = Instantiate(_birdPrefab, _parent);
            return bird;
        }
    }
}