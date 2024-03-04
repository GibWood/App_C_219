using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class HeroPlayerAudioContext : MonoBehaviour
    {
        [SerializeField] private AudioSource _hitPlayerAudio;
        [SerializeField] private AudioSource _groundAudio;
        
        [SerializeField] private AudioSource _goldTakeAudio;

        public void PlayPlayerHitAudio()
        {
            PlayAudio(_hitPlayerAudio);
        }

        public void PlayGroundAudio()
        {
            PlayAudio(_groundAudio);
        }

        public void PlayGoldTakeAudio()
        {
            PlayAudio(_goldTakeAudio);
        }

        private void PlayAudio(AudioSource audioSource)
        {
            audioSource.Play();
        }
    }
}