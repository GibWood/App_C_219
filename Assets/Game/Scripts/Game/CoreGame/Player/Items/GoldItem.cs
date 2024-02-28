using Game.Scripts.Game.CoreGame.Player.Player;

namespace Game.Scripts.Game.CoreGame.Player.Items
{
    public class GoldItem : BaseItem
    {
        public override void Collected(HeroPlayer heroPlayer)
        {
            heroPlayer.PlayerData.IncreaseGolds(_value);
            heroPlayer.HeroPlayerAudioContext.PlayGoldTakeAudio();
        }
    }
}