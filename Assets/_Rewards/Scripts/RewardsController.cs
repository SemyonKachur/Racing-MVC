using System;
using Profile;
using Tool;
using UnityEngine;

namespace Rewards
{
    internal class RewardsController : BaseController, IDisposable
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Daily Reward Window");
        private readonly ResourcePath _path = new ResourcePath("Prefabs/Currency Window");
        private readonly ProfilePlayer _profilePlayer;
        private readonly Transform _placeForUI;
        
        private DailyRewardController _dailyRewardController;
        private WeeklyRewardController _weeklyRewardController;
        private CurrencyView _currencyView;
        private DailyRewardView _rewardView;
        private PopUpView _popUpView;

        public RewardsController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));
            _placeForUI = placeForUi;
            _rewardView = LoadRewardMenuView();
            _popUpView = _rewardView.gameObject.GetComponent<PopUpView>();
            _currencyView = LoadCurrcenyView();
            _currencyView.Init();
            
            _dailyRewardController = new DailyRewardController(_rewardView);
            AddController(_dailyRewardController);
            _dailyRewardController.RefreshView();
            _dailyRewardController._mainMenu += Back;
            _weeklyRewardController = new WeeklyRewardController(_rewardView);
            AddController(_weeklyRewardController);
            _weeklyRewardController.RefreshView();
            
            _popUpView.ShowPopup();
        }

        private void Back()
        {
            var buttonBack = _rewardView.BackButton.GetComponent<CustomButton>();
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

        private CurrencyView LoadCurrcenyView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_path);
            GameObject objectView = GameObject.Instantiate(prefab, _placeForUI, false);
            AddGameObject(objectView);
            _currencyView = objectView.GetComponent<CurrencyView>();
            
            return _currencyView;
        }

        private DailyRewardView LoadRewardMenuView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = GameObject.Instantiate(prefab, _placeForUI, false);
            AddGameObject(objectView);
            _rewardView = objectView.GetComponent<DailyRewardView>();
            return _rewardView;
        }
        public void Dispose()
        {
            base.Dispose();
            _dailyRewardController._mainMenu -= Back;
        }

    }
}