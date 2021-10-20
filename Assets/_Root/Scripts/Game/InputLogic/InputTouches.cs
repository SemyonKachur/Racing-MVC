using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputTouches : BaseInputView
    {
        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);

        private void Move()
        {
            var speed = (_speed/2) * Time.deltaTime;
            if (Input.touchCount > 0 && Input.GetTouch(0).position.x >= Screen.width / 2)
            {
                speed = 1f;
                OnRightMove(speed*_speed);
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2)
            {
                speed = 0;
                OnRightMove(speed*_speed);
            }
            OnRightMove(speed);
        }
    }
}