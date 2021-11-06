using Profile;
using Tool;
using UnityEngine;

namespace AI
{
    internal class FightController : BaseController
    {
        private ResourcePath _path = new ResourcePath("Prefabs/Fight Window View");
        
        private readonly ProfilePlayer _profilePlayer;
        private readonly Transform _placeForUI;
        private readonly FightWindowView _view;
        
        public FightController(ProfilePlayer profilePlayer, Transform placeForUI)
        {
            _profilePlayer = profilePlayer;
            _placeForUI = placeForUI;

            _view = LoadView(_placeForUI);
            _view.Init();
        }
        
        private FightWindowView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_path);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FightWindowView>();
        }
    }
}