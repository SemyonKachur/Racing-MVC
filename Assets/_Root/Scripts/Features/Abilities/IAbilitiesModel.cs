using System.Collections.Generic;
using Inventory;

namespace Features.Abilities
{
    internal interface IAbilitiesModel
    {
        public List<IAbility> Abilities { get; }

        public IReadOnlyList<IAbility> GetEquippedItems();
    }
}