using System;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace Features.Abilities
{
    internal class AbilitiesView : MonoBehaviour, IAbilityCollectionView
    {
        [SerializeField] private GameObject _itemViewPrefab;
        [SerializeField] private Transform _placeForItems;

        private List<AbilitiesView> _abilitiesView;
        
        private void OnDestroy()
        {
            if (_abilitiesView == null)
                return;

            foreach (AbilitiesView abilitiesView in _abilitiesView)
                Destroy(abilitiesView.gameObject);

            _abilitiesView.Clear();
        }


        public void Init(IReadOnlyList<IAbility> itemInfoCollection)
        {
            _abilitiesView ??= new List<AbilitiesView>();
            foreach (IAbility item in itemInfoCollection)
            {
                AbilitiesView itemView = CreateItemView(item);
                _abilitiesView.Add(itemView);
            }
        }

        private AbilitiesView CreateItemView(IAbility item)
        {
            GameObject objectView = Instantiate(_itemViewPrefab, _placeForItems, false);
            AbilitiesView itemView = objectView.GetComponent<AbilitiesView>();
            return itemView;
        }

        public event EventHandler<IAbility> UseRequested;
        public void Display(IReadOnlyList<IAbility> abilityItems)
        {
            
        }
    }
}