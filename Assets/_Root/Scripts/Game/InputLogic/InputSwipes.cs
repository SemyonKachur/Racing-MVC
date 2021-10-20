using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputSwipes : BaseInputView
    {
        private Vector2 startPos;
        private Vector2 direction;
        private bool directionChosen;
        private TrailRenderer _trail;

        private void Start()
        {
            UpdateManager.SubscribeToUpdate(Move);
            _trail = GetComponent<TrailRenderer>();
        }

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);
        
        private void Move()
        {
            var speed = (_speed/2) * Time.deltaTime;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                _trail.gameObject.transform.position = Camera.main.ScreenToWorldPoint(touch.position);
                
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
                        speed = 1f;
                        OnRightMove(speed * _speed);
                    }
                }
                else
                {
                        speed = 0;
                        OnRightMove(speed*_speed);
                }
            }
            OnRightMove(speed);
        }
    }
}