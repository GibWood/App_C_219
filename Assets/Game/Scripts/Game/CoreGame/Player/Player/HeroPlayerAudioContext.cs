using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class HeroPlayerAudioContext : MonoBehaviour
    {
        [SerializeField] private AudioSource _hitPlayerAudio;
        [SerializeField] private AudioSource _deadPlayerAudio;
        
        [SerializeField] private AudioSource _coinTakeAudio;

        public void PlayPlayerHitAudio()
        {
            //PlayAudio(_hitPlayerAudio);
        }

        public void PlayDeadPlayerAudio()
        {
            PlayAudio(_deadPlayerAudio);
        }

        public void PlayGoldTakeAudio()
        {
            //PlayAudio(_coinTakeAudio);
        }

        private void PlayAudio(AudioSource audioSource)
        {
            audioSource.Play();
        }
    }
}