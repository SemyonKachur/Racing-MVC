using Profile;
using Services.Ads.UnityAds;
using Services.IAP;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/mainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private UnityAdsService _unityAds;
        private IAPService _iapService;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _unityAds = UnityAdsService.GetUnityAds();
            _iapService = IAPService.GetIAPService();
            _view = LoadView(placeForUi);
            _view.Init(StartGame,Settings,Reward,BuyItem);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }
       
        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void Settings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void Reward() => _unityAds.RewardedPlayer.Play();
        private void BuyItem() => _iapService.Buy("1");
    }
}
