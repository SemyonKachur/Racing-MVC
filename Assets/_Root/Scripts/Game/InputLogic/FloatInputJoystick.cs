using JoostenProductions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.InputLogic
{
    internal class FloatInputJoystick : BaseInputView, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private const float EnabledAlpha = 1;
        private const float DisabledAlpha = 0;

        [SerializeField] private Joystick _joystick;
        [SerializeField] private CanvasGroup _container;

        private bool _usingJoystick;


        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        public void OnPointerDown(PointerEventData eventData)
        {
            _joystick.transform.position = eventData.position;
            _joystick.SetStartPosition(eventData.position);
            _joystick.OnPointerDown(eventData);
            StartUsing();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystick.OnPointerUp(eventData);
            FinishUsing();
        }

        public void OnDrag(PointerEventData eventData) =>
            _joystick.OnDrag(eventData);

        private void StartUsing()
        {
            _usingJoystick = true;
            SetActive(true);
        }

        private void FinishUsing()
        {
            _usingJoystick = false;
            SetActive(false);
        }

        private void SetActive(bool active) =>
            _container.alpha = active ? EnabledAlpha : DisabledAlpha;

        private void Move()
        {
            if (!_usingJoystick)
                return;

            float moveStep = 10 * Time.deltaTime * CrossPlatformInputManager.GetAxis("Horizontal");

            if (moveStep > 0)
                OnRightMove(moveStep);
            else if (moveStep < 0)
                OnLeftMove(moveStep);
        }
    }
}
