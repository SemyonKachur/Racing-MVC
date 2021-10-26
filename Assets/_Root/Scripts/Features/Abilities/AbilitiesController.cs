using System;
using Inventory;
using JetBrains.Annotations;
using Tool;
using UnityEngine;

namespace Features.Abilities
{
    internal class AbilitiesController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Abilities/AbilitiesView");
        private readonly IAbilitiesModel _abilitiesModel;
        private readonly IAbilityRepository _abilityRepository;
        private readonly IAbilityCollectionView _abilityCollectionView;
        private readonly IAbilityActivator _abilityActivator;
        private readonly Transform _placeForUI;
        private readonly AbilitiesView _view;

        public AbilitiesController(
            IAbilityActivator abilityActivator,
            IAbilitiesModel abilitiesModel,
            IAbilityRepository abilityRepository,
            Transform placeForUI)
        {
            _abilityActivator = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));
            _abilitiesModel = abilitiesModel ?? throw new ArgumentNullException(nameof(abilitiesModel));
            _abilityRepository = abilityRepository ?? throw new ArgumentNullException(nameof(abilityRepository));
            _placeForUI = placeForUI;

            _view = LoadView(_placeForUI);
            _view.Init(abilitiesModel.GetEquippedItems());
            
            _abilityCollectionView.Display(abilitiesModel.GetEquippedItems());
            _abilityCollectionView.UseRequested += OnAbilityUseRequested;
        }
        
        private void OnAbilityUseRequested(object sender, IAbility e)
        {
            if (_abilityRepository.AbilityMapByItemId.TryGetValue(e.Id, out var ability))
                ability.Apply(_abilityActivator);
        }
        
        private AbilitiesView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }

    }

}