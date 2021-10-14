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
            var speed = 10 * Time.deltaTime;
            if (Input.touchCount > 0 && Input.GetTouch(0).position.x >= Screen.width / 2)
            {
                speed *= 1.3f;
                OnRightMove(speed);
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2)
            {
                speed /= 5;
                OnRightMove(speed);
            }
            OnRightMove(speed);
        }
    }
}