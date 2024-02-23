using CodeHub.OtherUtilities;
using Game.Mephistoss.PanelMachine.Scripts;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame
{
    public class TutorialEnabler : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private PanelMachine _panelMachine;
        [SerializeField] private PanelBase _tutorialPanel;
        private void Start()
        {
            CheckTutorial();
        }

        public void SkipTutorial()
        {
            _panelMachine.CloseLastPanel();
            //todo start game after tutorial
        }

        private void CheckTutorial()
        {
            if (_playerDatabase.HasSeenTutorial) return;
            
            _playerDatabase.HasSeenTutorial = true;
            _panelMachine.AddPanel(_tutorialPanel);
        }
    }
}
