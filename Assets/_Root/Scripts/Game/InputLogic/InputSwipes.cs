using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputSwipes : BaseInputView
    {
        public Vector2 startPos;
        public Vector2 direction;
        public bool directionChosen;
        
        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);
        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);
        
        private void Move()
        {
            var speed = 10 * Time.deltaTime;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        directionChosen = false;
                        break;

                    case TouchPhase.Moved:
                        direction = touch.position - startPos;
                        break;

                    case TouchPhase.Ended:
                        directionChosen = true;
                        break;
                }
            }
            if (directionChosen)
            {
                if (direction.x > direction.y)
                {
                    if (direction.x > 0)
                    {
                        speed *= 1.3f;
                        OnRightMove(speed);
                    }
                }
                else
                {
                        speed /= 5;
                        OnRightMove(speed);
                }
            }
            OnRightMove(speed);
        }
    }
}