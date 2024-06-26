using Features.Abilities;
using Game.Transport;
using Profile;
using Tool;
using UnityEngine;

namespace Game.Car
{
    internal class CarController : TransportController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Car");
        private readonly CarView _view;

        public GameObject ViewGameObject => _view.gameObject;

        public CarController() : base(TransportType.Car)
        {
            _view = LoadView();
        }
        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }
        public GameObject GetViewObject()
        {
            return ViewGameObject;
        }
    }
}
