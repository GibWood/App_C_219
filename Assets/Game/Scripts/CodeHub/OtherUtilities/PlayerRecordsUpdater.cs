using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class PlayerRecordsUpdater : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private List<TMP_Text> _playerRecordsTxt;

        private void Start()
        {
            _playerDatabase.OnPlayerRecordChange += UpdatePlayerRecordsTxt;
            UpdatePlayerRecordsTxt(_playerDatabase.PlayerCrystal);
        }

        private void UpdatePlayerRecordsTxt(int value)
        {
            foreach (var playerRecordTxt in _playerRecordsTxt)
            {
                if (playerRecordTxt != null) playerRecordTxt.text = value.ToString();
            }
        }
    }
}