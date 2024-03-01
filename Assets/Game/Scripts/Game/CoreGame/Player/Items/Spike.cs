using Game.Scripts.Game.CoreGame.Player.Player;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Items
{
    public class Spike : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<HeroPlayer>() == null) return;

            other.GetComponent<HeroPlayer>().GetDamage();
        }
    }
}