using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.DailyBonus
{
    public class DailyBonusUI : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _coinIcon;
        [SerializeField] private Text _dailyTxt;
        [SerializeField] private Outline _dailyOutline;
        [SerializeField] private Text _rewardTxt;
        [SerializeField] private Outline _rewardOutline;

        [SerializeField] private Sprite _disabledBackground;
        [SerializeField] private Sprite _enabledBackground;

        [SerializeField] private float _baseAlpha;
        [SerializeField] private float _outlineAlpha;

        public void EnableDailyBonus()
        {
            _background.sprite = _enabledBackground;
            SetEnableColor();
        }

        public void DisableDailyBonus()
        {
            _background.sprite = _disabledBackground;
            SetDisableColor();
        }

        private void SetDisableColor()
        {
            _coinIcon.color = new Color(_coinIcon.color.r, _coinIcon.color.g, _coinIcon.color.b, _baseAlpha);
            _dailyTxt.color = new Color(_dailyTxt.color.r, _dailyTxt.color.g, _dailyTxt.color.b, _baseAlpha);
            _rewardTxt.color = new Color(_rewardTxt.color.r, _rewardTxt.color.g, _rewardTxt.color.b, _baseAlpha);
            
            _dailyOutline.effectColor = new Color(_dailyOutline.effectColor.r, _dailyOutline.effectColor.g,
                _dailyOutline.effectColor.b, _outlineAlpha);
            _rewardOutline.effectColor = new Color(_rewardOutline.effectColor.r, _rewardOutline.effectColor.g,
                _rewardOutline.effectColor.b, _outlineAlpha);
        }

        private void SetEnableColor()
        {
            _coinIcon.color = Color.white;
            _dailyTxt.color = Color.white;
            _rewardTxt.color = Color.white;
            _dailyOutline.effectColor = Color.black;
            _rewardOutline.effectColor = Color.black;
        }
    }
}