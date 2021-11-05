using System;
using System.Collections.Generic;
using Configs.Shed;
using Inventory;
using JetBrains.Annotations;
using Profile;
using Tool;
using UnityEngine;

namespace Shed
{
    internal class ShedController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Shed/ShedView");

        private readonly ProfilePlayer _profilePlayer;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;
        private readonly InventoryController _inventoryController;
        private readonly ShedView _view;
        private PopUpView _popUpView;


        public ShedController(
            Transform placeForUi,
            ProfilePlayer profilePlayer,
            IReadOnlyList<UpgradeItemConfig> upgradeItemConfigs)
        
        {
            if (upgradeItemConfigs == null)
                throw new ArgumentNullException(nameof(upgradeItemConfigs));

            _profilePlayer = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _upgradeHandlersRepository = new UpgradeHandlersRepository(upgradeItemConfigs);
            AddRepository(_upgradeHandlersRepository);

            _inventoryController = new InventoryController(placeForUi, _profilePlayer.Inventory);
            AddController(_inventoryController);

            _view = LoadView(placeForUi);
            _view.Init(Apply, Back);

            _popUpView = _view.gameObject.GetComponent<PopUpView>();
            _popUpView._rect = _inventoryController.View.gameObject.GetComponent<RectTransform>();
            _popUpView.ShowPopup();
        }

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

        private void Apply()
        {
            var button = _view.ButtonApply.gameObject.GetComponent<CustomButton>();
            button._animationEnd += ChangeState;
            button.ActivateAnimation();
        }
        
        private void ChangeState(GameState gameState)
        {
            _popUpView.AnimationComplete += Change;
            _popUpView.HidePopup();
            
            void Change()
            {
                UpgradeCarWithEquippedItems(
                    _profilePlayer.CurrentTransport,
                    _profilePlayer.Inventory.GetEquippedItems(),
                    _upgradeHandlersRepository.UpgradeItems);

                _profilePlayer.CurrentState.Value = GameState.Start;
                Log($"Apply. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
            }
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Back. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
        }

        private void UpgradeCarWithEquippedItems(
            IUpgradableTransport upgradableTransport,
            IReadOnlyList<IItem> equippedItems,
            IReadOnlyDictionary<int, IUpgradeTransportHandler> upgradeHandlers)
        {
            foreach (var equippedItem in equippedItems)
                if (upgradeHandlers.TryGetValue(equippedItem.Id, out var handler))
                {
                    handler.Upgrade(upgradableTransport);
                    Log($"Upgrade {equippedItem.Info.Title}. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
                }
        }

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}
