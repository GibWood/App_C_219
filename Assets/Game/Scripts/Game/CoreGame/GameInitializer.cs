using Game.Scripts.Game.CoreGame.Player.Player;
using UnityEngine;

namespace Game.Scripts.Game.CoreGame
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private HeroPlayer _heroPlayer;
        [SerializeField] private CameraShaker _cameraShaker;
        [SerializeField] private GameOverContext _gameOverContext;

        private void Awake() =>
            Initialize();

        private void Initialize()
        {
            _heroPlayer.Initialize(1, _cameraShaker);
            _heroPlayer.PlayerData.OnDeath += EndGame;
        }

        private void EndGame()
        {
            _heroPlayer.gameObject.SetActive(false);
            _gameOverContext.AddGameOverPanel();
        }
    }
}