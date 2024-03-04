using DG.Tweening;
using Game.Mephistoss.PanelMachine.Scripts;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;

namespace Game.Scripts.Game.CoreGameLogic
{
    public class LeaveGameContext : MonoBehaviour
    {
        [SerializeField] private PanelMachine _panelMachine;
        [SerializeField] private PanelBase _leaveGamePanel;
        [SerializeField] private PanelBase _exitPanel;
        [SerializeField] private PanelBase _restartPanel;

        public void AddPause()
        {
            _panelMachine.AddPanel(_leaveGamePanel);
            Time.timeScale = 0;
            DOTween.timeScale = 0;
        }

        public void ContinueGame()
        {
            Time.timeScale = 1;
            DOTween.timeScale = 1;

            _panelMachine.CloseLastPanel();
        }

        public void GoToMenu()
        {
            Time.timeScale = 1;
            DOTween.timeScale = 1;
            DOTween.KillAll();

            SceneLoader.Instance.SwitchToScene("Menu");
        }

        public void AddExitPanel()
        {
            _panelMachine.CloseLastPanel();
            _panelMachine.AddPanel(_exitPanel);
        }
        public void AddRestartPanel()
        {
            _panelMachine.CloseLastPanel();
            _panelMachine.AddPanel(_restartPanel);
        }

        public void ReturnToPause()
        {
            _panelMachine.CloseLastPanel();
            _panelMachine.AddPanel(_leaveGamePanel);
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            DOTween.timeScale = 1;
            DOTween.KillAll();

            SceneLoader.Instance.SwitchToScene("Game");
        }
    }
}