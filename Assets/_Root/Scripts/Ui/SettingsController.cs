using Tool;
using Profile;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{ 
    internal class SettingsController : BaseController
    {
            private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/settingsMenu");
            private readonly ProfilePlayer _profilePlayer;
            private readonly SettingsView _view;
            private readonly PopUpView _popUpView;

        public SettingsController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(Back);
            _popUpView = _view.GetComponent<PopUpView>();
            _popUpView.ShowPopup();
        }

        private SettingsView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsView>();
        }


        private void Back()
        {
            var buttonBack = _view.ButtonBack.GetComponent<CustomButton>();
            buttonBack.AnimationEnd += ChangeState;
            buttonBack.ActivateAnimation();
        }

        private void ChangeState(GameState gameState)
        {
            _popUpView.AnimationComplete += Change;
            _popUpView.HidePopup();
            
            void Change()
            {
                _profilePlayer.CurrentState.Value = GameState.Start;
            }
        }
        
    }
}