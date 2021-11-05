using System;
using JetBrains.Annotations;
using Tool;
using UnityEngine;

namespace Inventory
{
    internal class InventoryController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Inventory/InventoryView");
        private readonly ItemsRepository _repository;
        private readonly InventoryModel _model;
        private readonly InventoryView _view;
        public InventoryView View => _view;
        
        public InventoryController(
            [NotNull] Transform placeForUi,
            [NotNull] InventoryModel inventoryModel)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _model = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));

            _view = LoadView(placeForUi);
            _view.Init(_model.GetEquippedItems());
        }

        private InventoryView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }
    }
}
