using Features.Abilities.Items;
using Inventory;

namespace Features.Abilities
{
    internal class AbilitiesInitializer
    {
        private readonly AbilitiesModelConfig _abilitiesModelConfig;

        public AbilitiesInitializer(AbilitiesModelConfig abilitiesModelConfig)
        { 
            _abilitiesModelConfig = abilitiesModelConfig;
        }


        public void InitializeModel(IAbilitiesModel abilitiesModel)
        {
            foreach ( AbilityItemConfig abilityConfig in _abilitiesModelConfig.AbilityList)
            {
                var ability = CreateAbility(abilityConfig);
                abilitiesModel.EquipItem(ability);
            }
        }

        private IAbility CreateAbility(AbilityItemConfig abilityConfig)
        {
            var itemInfo = new ItemInfo(abilityConfig.itemConfig.Title, abilityConfig.itemConfig.Icon);
            return new Ability(abilityConfig.Id, itemInfo);
        }
    }
}