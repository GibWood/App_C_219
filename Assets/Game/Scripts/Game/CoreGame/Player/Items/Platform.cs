using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Items
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GoldItem _goldItem;
        [SerializeField] private int _chanceDropItem=10;
        
        public void InitForUse()
        {
            ActivateGoldItem();
        }

        private void ActivateGoldItem()
        {
            _goldItem.gameObject.SetActive(false);
            int chance = Random.Range(0, 100);
            
            if (chance <= _chanceDropItem) 
                _goldItem.gameObject.SetActive(true);
        }
    }
}