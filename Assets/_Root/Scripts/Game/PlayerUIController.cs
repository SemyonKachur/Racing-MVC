using AI;
using Profile;
using Tool;
using UnityEngine;

namespace Game
{
    internal class PlayerUIController : BaseController
    {
        private ResourcePath _buttonFightViewPath = new ResourcePath("Prefabs/PlayerGameUI");

        private readonly ProfilePlayer _playerProfile;
        private readonly Transform _placeForUI;
        private readonly FightWindowView _fightWindowView;
        private readonly PlayerGameUIView _playerGameUIView;
        

        public PlayerUIController(ProfilePlayer profilePlayer,Transform placeForUI)
        {
            _placeForUI = placeForUI;
            _playerProfile = profilePlayer;

            _playerGameUIView = LoadButtonView(_placeForUI);
            _playerGameUIView.Init(StartFight);
        }
        
        private PlayerGameUIView LoadButtonView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_buttonFightViewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PlayerGameUIView>();
        }
        
        private void StartFight()
        {
            var button = _playerGameUIView.FightButton.gameObject.GetComponent<CustomButton>();
            button.AnimationEnd += ChangeState;
            button.ActivateAnimation();
        }
        
        private void ChangeState(GameState gameState)
        {
            _playerProfile.CurrentState.Value = GameState.Fight;
        }
    }
}