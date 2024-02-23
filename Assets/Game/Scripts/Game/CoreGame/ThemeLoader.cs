using Game.Scripts.Game.ShopLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.CoreGame
{
    public class ThemeLoader : MonoBehaviour
    {
        [SerializeField] private ShopElementContextData _contextData;
        [SerializeField] private Image _background;

        private void Start()
        {
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var currentData = _contextData.GetCurrentElement();
            _background.sprite = currentData.LevelTheme;
        }
    }
}