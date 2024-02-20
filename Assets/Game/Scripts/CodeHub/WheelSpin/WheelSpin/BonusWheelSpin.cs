using System;
using CodeHub.WheelSpinLogic;
using Game.Mephistoss.PanelMachine.Scripts;
using UnityEngine;

namespace Game.Scripts.Game.WheelSpin
{
    public class BonusWheelSpin : MonoBehaviour
    {
        [SerializeField] private CodeHub.WheelSpinLogic.WheelSpin wheelSpin;
        [SerializeField] private UniversalPanel wheelSpinPanel;
        [SerializeField] private PanelMachine panelMachine;
        [SerializeField] private WheelSectorData coinData;
        [SerializeField] private WheelSectorData oopsData;
        [SerializeField] private WheelSectorData playOnData;
        [SerializeField] private WheelSectorData plusSpinData;

        public Action OnStartSpin { get; set; }
        public Action OnEndSpin { get; set; }
        public Action<WheelSector> OnWinCoin { get; set; }
        public Action<WheelSector> OnWinOops { get; set; }
        public Action<WheelSector> OnWinPlayOn { get; set; }
        public Action<WheelSector> OnWinPlusSpin { get; set; }

        public void OpenWheelSpin()
        {
            panelMachine.AddPanel(wheelSpinPanel);
        }

        public void CloseWheelSpin()
        {
            panelMachine.CloseLastPanel();
        }

        public void TrySpin()
        {
            OnStartSpin?.Invoke();
            wheelSpin.TrySpin((sector =>
            {
                OnEndSpin?.Invoke();
                CheckWheelSector(sector);
            }));
        }

        private void CheckWheelSector(WheelSector wheelSector)
        {
            if (wheelSector.WheelSectorData == coinData)
            {
                OnWinCoin?.Invoke(wheelSector);
            }

            if (wheelSector.WheelSectorData == oopsData)
            {
                OnWinOops?.Invoke(wheelSector);
            }

            if (wheelSector.WheelSectorData == playOnData)
            {
                OnWinPlayOn?.Invoke(wheelSector);
            }

            if (wheelSector.WheelSectorData == plusSpinData)
            {
                OnWinPlusSpin?.Invoke(wheelSector);
            }
        }
    }
}