using Tool;
using UnityEngine;

namespace Game.TapeBackground
{
    internal class TapeBackgroundController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/background");

        private TapeBackgroundView _view;
        private readonly SubscriptionProperty<float> _diff;
        private readonly ISubscriptionProperty<float> _leftMove;
        private readonly ISubscriptionProperty<float> _rightMove;

        public TapeBackgroundController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove)
        {
            _view = LoadView();
            _diff = new SubscriptionProperty<float>();

            _leftMove = leftMove;
            _rightMove = rightMove;

            _view.Init(_diff);

            _leftMove.SubscribeOnChange(Move);
            _rightMove.SubscribeOnChange(Move);
        }

        protected override void OnDispose()
        {
            _leftMove.UnSubscribeOnChange(Move);
            _rightMove.UnSubscribeOnChange(Move);
        }

        private TapeBackgroundView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<TapeBackgroundView>();
        }

        private void Move(float value)
        {
            _diff.Value = value;
        }
    }
}
