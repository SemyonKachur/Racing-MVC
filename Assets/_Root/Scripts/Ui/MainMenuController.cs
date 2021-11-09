using System;
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

        private PopUpView _popUpView;
        private UnityAdsService _unityAds;
        private IAPService _iapService;
       
        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _unityAds = UnityAdsService.GetUnityAds();
            _iapService = IAPService.GetIAPService();
            _view = LoadView(placeForUi);
            _view.Init(StartGame, Settings, Reward, BuyItem, BuyOil, Shed, RewardsMenu, 
                profilePlayer.Gold, 
                profilePlayer.Oil);
            _popUpView = _view.GetComponent<PopUpView>();
            _popUpView.ShowPopup();
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }
        
        private void StartGame()
        {
            var button = _view.ButtonStart.gameObject.GetComponent<CustomButton>();
            button._animationEnd += ChangeState;
            button.ActivateAnimation();
        }
        
        private void Settings()
        {
            var button = _view.ButtonSettings.gameObject.GetComponent<CustomButton>();
            button._animationEnd += ChangeState;
            button.ActivateAnimation();
        }
         
        private void Shed()
        { 
            var button = _view.ButtonShed.gameObject.GetComponent<CustomButton>();
            button._animationEnd += ChangeState;
            button.ActivateAnimation();
        }
        private void RewardsMenu()
        {
            var button = _view.ButtonRewards.gameObject.GetComponent<CustomButton>();
            button._animationEnd += ChangeState;
            button.ActivateAnimation();
        }
    
        private void Reward() => _unityAds.RewardedPlayer.Play();

        private void BuyItem()
        {
            _iapService.Buy("1"); 
            _profilePlayer.AddGold(100);
            _view.GoldCount.text = _profilePlayer.Gold.ToString();
        }
        private void BuyOil()
        {
            _iapService.Buy("2");
            _profilePlayer.AddOil(100);
            _view.OilCount.text = _profilePlayer.Oil.ToString();
        }
        private void ChangeState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Settings:
                    _profilePlayer.CurrentState.Value = GameState.Settings;
                    break;
                case GameState.Start:
                    _profilePlayer.CurrentState.Value = GameState.Start;
                    break;
                case GameState.Game:
                    _profilePlayer.CurrentState.Value = GameState.Game;
                    break;
                case GameState.Shed:
                    _profilePlayer.CurrentState.Value = GameState.Shed;
                    break;
                case GameState.Rewards:
                    _profilePlayer.CurrentState.Value = GameState.Rewards;
                    break;
                case GameState.Quit:
                    _profilePlayer.CurrentState.Value = GameState.Quit;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }
    }
}
