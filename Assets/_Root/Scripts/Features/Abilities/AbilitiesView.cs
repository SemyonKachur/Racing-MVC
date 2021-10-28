using System;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Abilities
{
    internal class AbilitiesView : MonoBehaviour, IAbilityCollectionView
    {
        [SerializeField] private GameObject _itemViewPrefab;
        [SerializeField] private Transform _placeForItems;

        private List<ItemView> _abilitiesView;
        public List<Button> _buttonView;

        private void OnDestroy()
        {
            if (_abilitiesView == null)
                return;

            foreach (ItemView abilitiesView in _abilitiesView)
                Destroy(abilitiesView.gameObject);

            _abilitiesView.Clear();
        }

        private ItemView CreateItemView(IAbility item)
        {
            GameObject objectView = Instantiate(_itemViewPrefab, _placeForItems, false);
            ItemView abilityItem = objectView.gameObject.GetComponent<ItemView>();
            abilityItem.Init(item);
            return abilityItem;
        }

        public event EventHandler<IAbility> UseRequested;
        public void Display(IReadOnlyList<IAbility> abilityItems)
        {
            _abilitiesView ??= new List<ItemView>();
            _buttonView ??= new List<Button>();
            foreach (IAbility item in abilityItems)
            {
                ItemView itemView = CreateItemView(item);
                _abilitiesView.Add(itemView);
            }
        }
    }
}