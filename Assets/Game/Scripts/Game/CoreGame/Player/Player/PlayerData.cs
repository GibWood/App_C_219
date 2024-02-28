using System;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class PlayerData
    {
        private int _hp;
        private int _golds;
        private int _maxHp;

        public int Hp => _hp;
        public int Golds => _golds;

        public Action OnDeath;
        public Action OnLevelUp;
        public Action<int> OnChangeGolds;

        public int MaxPlayerHp => _maxHp;

        public PlayerData(int maxHp)
        {
            _hp = maxHp;
            _maxHp = maxHp;
            _golds = 0;
        }

        public void IncreaseGolds(int value)
        {
            _golds += value;
            OnChangeGolds?.Invoke(_golds);
        }

        public void IncreaseHp(int value)
        {
            int newHpValue = _hp + value;
            _hp = Math.Clamp(newHpValue, 0, _maxHp);
            if (_hp <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void SetHpValue(int value) => 
            _hp = value;

        public void LevelUp() => 
            OnLevelUp?.Invoke();
    }
}