using Features.Abilities;
using Tool;
using UnityEngine;

namespace Game.Boat
{
    internal class BoatController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Boat");
        private readonly BoatView _view;

        public GameObject ViewGameObject => _view.gameObject;

        public BoatController()
        {
            _view = LoadView();
        }
        private BoatView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<BoatView>();
        }
        public GameObject GetViewObject()
        {
            return ViewGameObject;
        }
    }
}
