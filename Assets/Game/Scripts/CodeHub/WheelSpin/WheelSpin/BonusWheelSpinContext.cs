using CodeHub.OtherUtilities;
using CodeHub.WheelSpinLogic;
using Game.Mephistoss.PanelMachine.Scripts;
using Prototype.SceneLoaderCore.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.WheelSpin
{
    public class BonusWheelSpinContext : MonoBehaviour
    {
        [SerializeField] private PanelMachine _panelMachine;

        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private BonusWheelSpin _bonusWheelSpin;

        [SerializeField] private Button _spinBtn;
        [SerializeField] private Button _backBtn;
        [SerializeField] private TMP_Text _spinCountTxt;
        [SerializeField] private TMP_Text _rewardCoinTxt;
        [SerializeField] private TMP_Text _rewardTxt;

        [SerializeField] private AudioSource _spin;
        [SerializeField] private AudioSource _onPlay;
        [SerializeField] private AudioSource _coin;
        [SerializeField] private AudioSource _win;
        [SerializeField] private AudioSource _loose;

        private int _bonusSpinCount = 0;
        private AnimationService _animationService;

        public int TotalSpin => _bonusSpinCount;

        private void Start()
        {
            _bonusWheelSpin.OnStartSpin += StartSpin;
            _bonusWheelSpin.OnEndSpin += EndSpin;
            _bonusWheelSpin.OnWinCoin += WinCoin;
            _bonusWheelSpin.OnWinOops += WinOops;
            _bonusWheelSpin.OnWinPlayOn += WinPlayOn;
            _bonusWheelSpin.OnWinPlusSpin += WinPlusSpin;

            _animationService = new AnimationService();
        }

        private void OnEnable()
        {
            UpdateUiElements();
        }

        public void TryOpenBonusSpin()
        {
            if (TotalSpin != 0)
            {
                _bonusWheelSpin.OpenWheelSpin();
            }
        }

        public void AddSpin(int count)
        {
            _bonusSpinCount = count;
        }

        public void SpinClaim()
        {
            if (_bonusSpinCount != 0)
            {
                _bonusSpinCount--;
            }
        }

        private async void WinCoin(WheelSector wheelSector)
        {
            _playerDatabase.IncreasePlayerBalance(wheelSector.Value);
            DisableSpinControlElements();
            EnableWinCoinReward(wheelSector.Value);
            _coin.Play();

            await _animationService.PlayDeflateAnimation(_rewardCoinTxt.gameObject);

            EnableSpinControlElements();
            UpdateUiElements();
            SceneLoader.Instance.SwitchToScene("Menu");
        }

        private async void WinOops(WheelSector wheelSector)
        {
            DisableSpinControlElements();
            EnableReward("ooops..");
            _loose.Play();

            await _animationService.PlayDeflateAnimation(_rewardTxt.gameObject);

            EnableSpinControlElements();
            UpdateUiElements();
            SceneLoader.Instance.SwitchToScene("Menu");
        }

        private async void WinPlayOn(WheelSector wheelSector)
        {
            DisableSpinControlElements();
            EnableReward("Play on..");
            _onPlay.Play();

            await _animationService.PlayDeflateAnimation(_rewardTxt.gameObject);
            _panelMachine.CloseLastPanel(); //close wheel spin panel
            _panelMachine.CloseLastPanel(); //close choose spin panel

            EnableSpinControlElements();
            UpdateUiElements();
        }

        private async void WinPlusSpin(WheelSector wheelSector)
        {
            DisableSpinControlElements();
            EnableReward("SPIN");
            _win.Play();

            await _animationService.PlayDeflateAnimation(_rewardTxt.gameObject);
            _bonusSpinCount++;

            EnableSpinControlElements();
            UpdateUiElements();
        }

        private void DisableSpinControlElements()
        {
            _spinBtn.gameObject.SetActive(false);
            _spinCountTxt.gameObject.SetActive(false);
            _backBtn.interactable = false;
        }

        private void EnableWinCoinReward(int value)
        {
            _rewardTxt.gameObject.SetActive(false);
            _rewardCoinTxt.gameObject.SetActive(true);
            _rewardCoinTxt.text = value + "";
        }

        private void EnableReward(string reward)
        {
            _rewardTxt.gameObject.SetActive(true);
            _rewardCoinTxt.gameObject.SetActive(false);
            _rewardTxt.text = reward;
        }

        private void UpdateUiElements()
        {
            _spinCountTxt.text = "You have " + TotalSpin + " attempts";
            _spinBtn.interactable = TotalSpin != 0;
        }

        private void EnableSpinControlElements()
        {
            _spinBtn.gameObject.SetActive(true);
            _spinCountTxt.gameObject.SetActive(true);

            _rewardTxt.gameObject.SetActive(false);
            _rewardCoinTxt.gameObject.SetActive(false);

            _backBtn.interactable = true;
        }

        private void StartSpin()
        {
            _backBtn.interactable = false;
            SpinClaim();
            UpdateUiElements();
            _spinBtn.gameObject.SetActive(false);

            _spin.Play();
        }

        private void EndSpin()
        {
            _spin.Stop();
        }
    }
}