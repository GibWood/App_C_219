using Game.Scripts.Game.CoreGame.Player.Player;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Items
{
    public class BaseItem : MonoBehaviour, ICollected
    {
        [SerializeField] protected int _value;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<HeroPlayer>() == null) return;

            Collected(other.GetComponent<HeroPlayer>());

            gameObject.SetActive(false);
        }

        public virtual void Collected(HeroPlayer heroPlayer)
        {
            throw new System.NotImplementedException();
        }
    }
}