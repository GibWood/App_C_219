using System.Linq;
using Game.Scripts.Game.CoreGame.BirdsLogic;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] private HeroMover _heroMover;
        [SerializeField] private HookLine _hookLine;
        [SerializeField] private HookBtn _hookBtn;
        [SerializeField] private SpinePlatformerAnimation2D _spine;

        [SerializeField] private BirdsSpawner _birdsSpawner;


        [SerializeField] private float _offset = 0.5f;

        private Bird _currentBird;

        private void Start()
        {
            _hookBtn.OnDown += MakeHook;
            _hookBtn.OnUp += StopMakeHook;
            
            _hookLine.EnableLineRenderer(false);
            _hookLine.OnCaptured += CapturedBird;

            _heroMover.OnEndMove += StopMakeHook;
        }

        private void OnDestroy()
        {
            _hookBtn.OnDown -= MakeHook;
            _hookBtn.OnUp -= StopMakeHook;
            _hookLine.OnCaptured -= CapturedBird;
            _heroMover.OnEndMove -= StopMakeHook;
        }

        private void MakeHook()
        {
            _currentBird = NearestBird();
            if (_currentBird == null) return;

            _hookLine.MoveLine(true, _currentBird.transform);
            _hookLine.EnableLineRenderer(true);
            
            _spine.PlayThrowAnimation();
        }

        private void CapturedBird()
        {
            _currentBird.GetComponent<BirdMove>().SetSlowSpeed();
            _currentBird.Capture();
            _heroMover.MoveHero(_currentBird.transform);
            
            _spine.PlayFlyAnimation();
        }

        private void StopMakeHook()
        {
            _heroMover.StopMove();
            _hookLine.MoveLine(false);
            _hookLine.EnableLineRenderer(false);

            if (_currentBird != null && _currentBird.Captured) 
                _currentBird.GetComponent<BirdMove>().MoveAway = true;

            _currentBird = null;
            
            _spine.PlayIdleAnimation();
        }

        private Bird NearestBird()
        {
            if (_birdsSpawner.Birds.Count == 0)
                return null;

            var rightBirds = _birdsSpawner.Birds.Where(bird =>
                bird.transform.position.x > _heroMover.transform.position.x - _offset && bird.Captured == false);

            if (rightBirds.Count() == 0)
                return null;

            var nearestBird = rightBirds
                .OrderBy(bird => Vector3.Distance(bird.transform.position, _heroMover.transform.position)).First();
            return nearestBird;
        }
    }
}