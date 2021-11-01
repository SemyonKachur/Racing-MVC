using System;
using Profile;
using Tool;
using UnityEngine;
using Object = System.Object;

namespace Rewards
{
    internal class RewardsController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Daily Reward Window");
        private readonly ResourcePath _path = new ResourcePath("Prefabs/Currency Window");
        private readonly ProfilePlayer _profilePlayer;
        private readonly Transform _placeForUI;
        
        private DailyRewardController _dailyRewardController;
        private CurrencyView _currencyView;
        private DailyRewardView _dailyRewardView;
      
        public RewardsController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));
            _placeForUI = placeForUi;
            _dailyRewardView = LoadRewardMenuView();
            _currencyView = LoadCurrcenyView();
            _currencyView.Init();
            _dailyRewardController = new DailyRewardController(_dailyRewardView);
            _dailyRewardController.RefreshView();
        }

        private CurrencyView LoadCurrcenyView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_path);
            GameObject objectView = GameObject.Instantiate(prefab, _placeForUI, false);
            _currencyView = objectView.GetComponent<CurrencyView>();
            return _currencyView;
        }

        private DailyRewardView LoadRewardMenuView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = GameObject.Instantiate(prefab, _placeForUI, false);
            _dailyRewardView = objectView.GetComponent<DailyRewardView>();
            return _dailyRewardView;
        }
        
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

    }
}