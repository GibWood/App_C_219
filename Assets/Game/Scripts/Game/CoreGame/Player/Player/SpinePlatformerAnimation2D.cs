using DG.Tweening;
using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class SpinePlatformerAnimation2D : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        [SerializeField] private PlatformerMotor2D platformerMotor2D;
        [SerializeField] private AnimationReferenceAsset idle;
        [SerializeField] private AnimationReferenceAsset jump;
        [SerializeField] private AnimationReferenceAsset fall;
        [SerializeField] private AnimationReferenceAsset slipping;
        [SerializeField] private AnimationReferenceAsset walk;
        [SerializeField] private AnimationReferenceAsset getHit;
        [SerializeField] private AnimationReferenceAsset attack;

        public SkeletonAnimation SkeletonAnimation => skeletonAnimation;

        private PlatformerMotor2D _motor;
        private bool _isJumping;

        private bool _canMakeDefaultAnimation;

        private Tween delayedCall;

        // Use this for initialization
        private void Start()
        {
            _motor = platformerMotor2D;
            _canMakeDefaultAnimation = true;
            //todo change 
            return;
            skeletonAnimation.state.SetAnimation(0, idle, true);
        }

        // Update is called once per frame
        private void Update()
        {
            return;
            if (_motor.motorState == PlatformerMotor2D.MotorState.Jumping ||
                _isJumping &&
                (_motor.motorState == PlatformerMotor2D.MotorState.Falling ||
                 _motor.motorState == PlatformerMotor2D.MotorState.FallingFast))
            {
                _isJumping = true;

                TrySetAnimation(jump);
            }
            else
            {
                _isJumping = false;
                skeletonAnimation.transform.rotation = Quaternion.identity;

                if (_motor.motorState == PlatformerMotor2D.MotorState.Falling ||
                    _motor.motorState == PlatformerMotor2D.MotorState.FallingFast)
                {
                    TrySetAnimation(fall);
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.Slipping)
                {
                    TrySetAnimation(slipping);
                }
                else
                {
                    if (_motor.velocity.sqrMagnitude >= 0.1f * 0.1f)
                    {
                        TrySetAnimation(walk);
                    }
                    else
                    {
                        TrySetAnimation(idle);
                    }
                }
            }

            // Facing
            float valueCheck = _motor.normalizedXMovement;

            if (_motor.motorState == PlatformerMotor2D.MotorState.Slipping ||
                _motor.motorState == PlatformerMotor2D.MotorState.Dashing ||
                _motor.motorState == PlatformerMotor2D.MotorState.Jumping)
            {
                valueCheck = _motor.velocity.x;
            }

            if (Mathf.Abs(valueCheck) >= 0.1f)
            {
                Vector3 newScale = skeletonAnimation.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * ((valueCheck > 0) ? 1.0f : -1.0f);
                skeletonAnimation.transform.localScale = newScale;
            }
        }

        public void PlayHitAnimation()
        {
            skeletonAnimation.state.SetAnimation(0, getHit, false);
            StopMakeAnimationForDelay(getHit.Animation.Duration);
        }

        public void PlayAttackAnimation()
        {
            skeletonAnimation.state.SetAnimation(0, attack, false);
            StopMakeAnimationForDelay(attack.Animation.Duration);
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