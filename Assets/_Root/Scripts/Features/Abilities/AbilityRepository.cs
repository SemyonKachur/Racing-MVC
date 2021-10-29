using System.Collections.Generic;
using Features.Abilities.Items;
using Inventory;

namespace Features.Abilities
{
    internal class AbilityRepository : IAbilityRepository
    {
        public IReadOnlyDictionary<int, IAbility> AbilityMapByItemId { get; }
        
        private readonly Dictionary<int, IAbility> _abilitiesMapById = new Dictionary<int, IAbility>();

        
        public AbilityRepository(List<AbilityItemConfig> abilityItemConfigs) =>
            PopulateItems(ref _abilitiesMapById, abilityItemConfigs);

        public void Dispose() =>
            _abilitiesMapById.Clear();


        private void PopulateItems(ref Dictionary<int, IAbility> upgradeHandlersMapByType, List<AbilityItemConfig> configs)
        {
            foreach (var config in configs)
                if (!upgradeHandlersMapByType.ContainsKey(config.Id))
                    upgradeHandlersMapByType.Add(config.Id, CreateItem(config));
        }

        private IAbility CreateItem(AbilityItemConfig config)
        {
            var abilityInfo = new ItemInfo(config.itemConfig.Title, config.itemConfig.Icon);
            return new Ability (config.Id, abilityInfo);
        }
        
    }
}