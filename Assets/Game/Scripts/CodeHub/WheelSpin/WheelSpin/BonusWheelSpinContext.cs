using CodeHub.OtherUtilities;
using CodeHub.WheelSpinLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.WheelSpin
{
    public class BonusWheelSpinContext : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private BonusWheelSpin _bonusWheelSpin;

        [SerializeField] private Button _spinBtn;
        [SerializeField] private Button _dailySpinBtn;
        [SerializeField] private Button _okBtn;

        [SerializeField] private TMP_Text _rewardTxt;

        [SerializeField] private AudioSource _spin;
        [SerializeField] private AudioSource _coin;
        [SerializeField] private AudioSource _win;

        private int _bonusSpinCount = 0;
        private AnimationService _animationService;

        public int TotalSpin => _bonusSpinCount;

        private void Start()
        {
            _bonusWheelSpin.OnStartSpin += StartSpin;
            _bonusWheelSpin.OnEndSpin += EndSpin;
            _bonusWheelSpin.OnWinCoin += WinCoin;
            _bonusWheelSpin.OnWinPlusSpin += WinPlusSpin;

            _animationService = new AnimationService();
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
            _playerDatabase.BonusClaimed();
            _dailySpinBtn.interactable = false;
            if (_bonusSpinCount != 0)
            {
                _bonusSpinCount--;
            }
        }

        private async void WinCoin(WheelSector wheelSector)
        {
            _playerDatabase.IncreasePlayerBalance(wheelSector.Value);
            UpdateSpinBtn();
            EnableWinCoinReward(wheelSector.Value);
            _coin.Play();

            await _animationService.PlayDeflateAnimation(_rewardTxt.gameObject);
            _okBtn.gameObject.SetActive(true);

            //SceneLoader.Instance.SwitchToScene("Menu");
        }

        private async void WinPlusSpin(WheelSector wheelSector)
        {
            EnableSpinReward("SPIN");
            _win.Play();

            await _animationService.PlayDeflateAnimation(_rewardTxt.gameObject);
            _bonusSpinCount++;

            EnableSpinControlElements();
            UpdateSpinBtn();
        }

        private void EnableWinCoinReward(int value)
        {
            _rewardTxt.gameObject.SetActive(true);
            _rewardTxt.text = value + "<sprite=0>";
        }

        private void EnableSpinReward(string reward)
        {
            _rewardTxt.gameObject.SetActive(true);
            _rewardTxt.text = reward;
        }

        private void UpdateSpinBtn()
        {
            _spinBtn.gameObject.SetActive(TotalSpin != 0);
        }

        private void EnableSpinControlElements()
        {
            _spinBtn.gameObject.SetActive(true);

            _rewardTxt.gameObject.SetActive(false);
        }

        private void StartSpin()
        {
            SpinClaim();
            UpdateSpinBtn();
            _spinBtn.gameObject.SetActive(false);

            _spin.Play();
        }

        private void EndSpin()
        {
            _spin.Stop();
        }
    }
}