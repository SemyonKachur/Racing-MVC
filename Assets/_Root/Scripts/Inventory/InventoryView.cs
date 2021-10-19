using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    internal class InventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _itemViewPrefab;
        [SerializeField] private Transform _placeForItems;

        private List<ItemView> _itemViews;


        private void OnDestroy()
        {
            if (_itemViews == null)
                return;

            foreach (ItemView itemView in _itemViews)
                Destroy(itemView.gameObject);

            _itemViews.Clear();
        }


        public void Init(IReadOnlyList<IItem> itemInfoCollection)
        {
            _itemViews ??= new List<ItemView>();
            foreach (IItem item in itemInfoCollection)
            {
                ItemView itemView = CreateItemView(item);
                _itemViews.Add(itemView);
            }
        }

        private ItemView CreateItemView(IItem item)
        {
            GameObject objectView = Instantiate(_itemViewPrefab, _placeForItems, false);
            ItemView itemView = objectView.GetComponent<ItemView>();
            itemView.Init(item);

            return itemView;
        }
    }
}
