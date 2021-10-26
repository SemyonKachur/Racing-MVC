using System.Collections.Generic;
using Inventory;

namespace Features.Abilities
{
    internal class AbilitiesModel : IAbilitiesModel, IAbilityRepository
    {
        private readonly List<IAbility> _abilities = new List<IAbility>();
        public IReadOnlyDictionary<int, IAbility> AbilityMapByItemId { get; }


        public IReadOnlyList<IAbility> GetEquippedItems() => _abilities;
        public List<IAbility> Abilities => _abilities;

        public void EquipItem(IAbility ability)
        {
            if (_abilities.Contains(ability))
                return;

            _abilities.Add(ability);
        }

        public void UnequipItem(IAbility item)
        {
            if (!_abilities.Contains(item))
                return;

            _abilities.Remove(item);
        }

    }
}