using DG.Tweening;
using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class SpinePlatformerAnimation2D : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        [SerializeField] private AnimationReferenceAsset idle;
        [SerializeField] private AnimationReferenceAsset fly;
        [SerializeField] private AnimationReferenceAsset land;
        [SerializeField] private AnimationReferenceAsset throwHook;

        public SkeletonAnimation SkeletonAnimation => skeletonAnimation;

        private bool _canMakeDefaultAnimation;

        private Tween delayedCall;

        // Use this for initialization
        private void Start()
        {
            _canMakeDefaultAnimation = true;
        }

        public void PlayFlyAnimation()
        {
            skeletonAnimation.state.SetAnimation(0, fly, true);
        }

        public void PlayIdleAnimation()
        {
            skeletonAnimation.state.SetAnimation(0, idle, true);
        }

        public void PlayLandAnimation()
        {
            skeletonAnimation.state.SetAnimation(0, land, false);
        }

        public void PlayThrowAnimation()
        {
            skeletonAnimation.state.SetAnimation(0, throwHook, false);
        }

        private void TrySetAnimation(AnimationReferenceAsset animation, bool loop = true)
        {
            if (skeletonAnimation.AnimationName != animation.name && _canMakeDefaultAnimation)
                skeletonAnimation.state.SetAnimation(0, animation, loop);
        }

        private void StopMakeAnimationForDelay(float delay)
        {
            _canMakeDefaultAnimation = false;

            if (delayedCall != null)
            {
                delayedCall.Kill();
            }

            delayedCall = DOVirtual.DelayedCall(delay, (EnableCanMakeAnimation));
        }

        private void EnableCanMakeAnimation()
        {
            _canMakeDefaultAnimation = true;
        }
    }
}