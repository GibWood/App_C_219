using CodeHub.OtherUtilities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.WheelSpin
{
    public class DailyBonusContext : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private BonusWheelSpinContext _bonusWheelSpin;
        [SerializeField] private Button _dailySpinBtn;
        private void Start()
        {
            UpdateDailyBtn();
        }

        private void UpdateDailyBtn()
        {
            _dailySpinBtn.interactable = _playerDatabase.HasBonusGame();
            if(_playerDatabase.HasBonusGame()) _bonusWheelSpin.AddSpin(1);
        }
    }
}