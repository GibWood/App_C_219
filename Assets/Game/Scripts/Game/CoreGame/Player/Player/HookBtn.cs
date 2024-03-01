using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Game.CoreGame.Player.Player
{
    public class HookBtn : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
    {
        public event Action OnUp;
        public event Action OnDown;
        public event Action OnClick;

        public void OnPointerUp(PointerEventData eventData) =>
            OnUp?.Invoke();

        public void OnPointerDown(PointerEventData eventData) =>
            OnDown?.Invoke();

        public void OnPointerClick(PointerEventData eventData) =>
            OnClick?.Invoke();
    }
}