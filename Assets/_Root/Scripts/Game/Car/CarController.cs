using Tool;
using UnityEngine;
using static Profile.ProfilePlayerSO;

namespace Game.Car
{
    internal class CarController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Car");
        private readonly CarView _view;

        public GameObject ViewGameObject => _view.gameObject;

        public CarController()
        {
            _view = LoadView();
        }
        public CarController(Transport transport)
        {
            if(transport == Transport.car) _viewPath = new ResourcePath("Prefabs/Car");
            else _viewPath = new ResourcePath("Prefabs/Boat");
            _view = LoadView();
        }

        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }
    }
}
