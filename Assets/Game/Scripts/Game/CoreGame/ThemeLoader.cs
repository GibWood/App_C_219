using System.Collections.Generic;
using Game.Scripts.Game.ShopLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.CoreGame
{
    public class ThemeLoader : MonoBehaviour
    {
        [SerializeField] private ShopElementContextData _contextData;
        [SerializeField] private Image _background;

        [SerializeField] private ShopElementData _japanTheme;
        [SerializeField] private List<GameObject> _japanObjects;
        [SerializeField] private List<GameObject> _nonJapanObjects;

        private void Start()
        {
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            ShopElementData currentData = _contextData.GetCurrentElement();
            _background.sprite = currentData.LevelTheme;
            UpdateThemeByJapan(currentData);
        }

        private void UpdateThemeByJapan(ShopElementData currentData)
        {
            if (currentData != _japanTheme) return;
            
            foreach (var nonJapanObject in _nonJapanObjects)
                nonJapanObject.gameObject.SetActive(false);

            foreach (var japanObject in _japanObjects)
                japanObject.gameObject.SetActive(true);
        }
    }
}