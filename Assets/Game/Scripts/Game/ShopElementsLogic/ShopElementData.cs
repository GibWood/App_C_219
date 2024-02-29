using System;
using Game.Scripts.Game.CoreGame.Level;
using UnityEngine;

namespace Game.Scripts.Game.ShopLogic
{
    [CreateAssetMenu(fileName = "ShopElementData", menuName = "ShopElementData", order = 1)]
    public class ShopElementData : ScriptableObject
    {
        [SerializeField] private int _price;
        [SerializeField] private Sprite _levelTheme;
        [SerializeField] private LevelPart _levelPrefab;

        private string _hasOpenAlias = "hasOpenAlias";

        public int Price => _price;
        public Sprite LevelTheme => _levelTheme;
        public LevelPart LevelPrefab => _levelPrefab;

        public bool HasOpen
        {
            get => Convert.ToBoolean(PlayerPrefs.GetString(_hasOpenAlias + name, "False"));
            set
            {
                PlayerPrefs.SetString(_hasOpenAlias + name, value.ToString());
                PlayerPrefs.Save();
            }
        }
    }
}