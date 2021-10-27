using Features.Abilities.Items;

namespace Features.Abilities
{
    internal class AbilitiesInitializer
    {
        private readonly AbilitiesModelConfig _abilitiesModelConfig;

        public AbilitiesInitializer(AbilitiesModelConfig abilitiesModelConfig) =>
            _abilitiesModelConfig = abilitiesModelConfig;


        public void InitializeModel(IAbilitiesModel abilitiesModel)
        {
            foreach ( AbilityItemConfig abilityConfig in abilitiesModel.Abilities)
            {
                var ability = CreateAbility(abilityConfig);
                abilitiesModel.EquipItem(ability);
            }
        }

        private IAbility CreateAbility(AbilityItemConfig abilityConfig)
        {
            var itemInfo = new AbilityInfo(abilityConfig.Title, abilityConfig.Icon);
            return new Ability(abilityConfig.Id, itemInfo);
        }
    }
}