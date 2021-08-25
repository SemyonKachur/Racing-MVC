using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputAcceleration : BaseInputView
    {
        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);

        private void Move()
        {
            Vector3 direction = CalcDirection();
            OnRightMove(direction.sqrMagnitude / 20 * _speed);
        }

        private Vector3 CalcDirection()
        {
            Vector3 direction = Vector3.zero;
            direction.x = -Input.acceleration.y;
            direction.z = Input.acceleration.x;

            if (direction.sqrMagnitude > 1)
                direction.Normalize();

            return direction;
        }
    }
}
