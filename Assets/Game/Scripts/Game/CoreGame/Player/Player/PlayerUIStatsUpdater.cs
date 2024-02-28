using TMPro;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class PlayerUIStatsUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _goldTxt;
        [SerializeField] private HeroPlayer _heroPlayer;

        private void Start() => 
            Initialize();

        private void Initialize()
        {
            UpdateGoldsTxt(_heroPlayer.PlayerData.Golds);
            _heroPlayer.PlayerData.OnChangeGolds += UpdateGoldsTxt;
        }

        private void UpdateGoldsTxt(int golds)
        {
            _goldTxt.text = golds + "";
        }
    }
}
