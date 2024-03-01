using System.Collections.Generic;
using CodeHub.OtherUtilities;
using Game.Mephistoss.PanelMachine.Scripts;
using Game.Scripts.Game.CoreGame.Player.Player;
using TMPro;
using Tools.Core.UnityAdsService.Scripts;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame
{
    public class GameOverContext : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private HeroPlayer _heroPlayer;

        [SerializeField] private PanelMachine _panelMachine;
        [SerializeField] private UniversalPanel _gameOverPanel;

        [SerializeField] private int _multiplyCoins = 10;

        [SerializeField] private TMP_Text _crystalCountTxt;
        [SerializeField] private TMP_Text _crystalNewRecordMark;

        [SerializeField] private TMP_Text _coinsCountTxt;
        [SerializeField] private TMP_Text _watchAdsTxtTxt;
        [SerializeField] private UnityAdsButton _watchAdsBtn;

        [SerializeField] private List<GameObject> _adsObjects;
        [SerializeField] private List<GameObject> _getRewardsObjects;

        private int _crystalCount;
        private int _coinsCount;

        private void Start()
        {
            _watchAdsBtn.OnCanGetReward += ClaimRewardFromAds;
        }

        public void AddGameOverPanel()
        {
            _panelMachine.AddPanel(_gameOverPanel);

            _crystalCount = _heroPlayer.PlayerData.Golds; //todo get crystal from game
            _coinsCount = _crystalCount * _multiplyCoins;
            UpdateUI();

            ClaimCoinsReward();
            UpdateDataCrystal();
        }

        private void UpdateDataCrystal()
        {
            if (_crystalCount > _playerDatabase.PlayerCrystal)
                _playerDatabase.PlayerCrystal = _crystalCount;
        }

        private void UpdateUI()
        {
            UpdateCrystalUI();
            UpdateCoinsUI();
            UpdateAdsTxtUI();
        }

        private void UpdateCrystalUI()
        {
            _crystalCountTxt.text = _crystalCount.ToString();
            _crystalNewRecordMark.gameObject.SetActive(_crystalCount > _playerDatabase.PlayerCrystal);
            if (_crystalCount == 0)
                DisableAdsObjects();
        }

        private void UpdateCoinsUI() =>
            _coinsCountTxt.text = _coinsCount.ToString();

        private void UpdateAdsTxtUI() =>
            _watchAdsTxtTxt.text = $"Watch the ad and get {_coinsCount * 2} <sprite=0>";

        private void ClaimRewardFromAds()
        {
            ClaimCoinsReward();
            _coinsCount *= 2;
            UpdateCoinsUI();

            DisableAdsObjects();
        }

        private void DisableAdsObjects()
        {
            _watchAdsBtn.gameObject.SetActive(false);
            _watchAdsTxtTxt.gameObject.SetActive(false);

            foreach (var adsObject in _adsObjects)
                adsObject.gameObject.SetActive(false);

            foreach (var rewardObjects in _getRewardsObjects)
                rewardObjects.gameObject.SetActive(true);
        }

        private void ClaimCoinsReward() =>
            _playerDatabase.IncreasePlayerBalance(_coinsCount);
    }
}