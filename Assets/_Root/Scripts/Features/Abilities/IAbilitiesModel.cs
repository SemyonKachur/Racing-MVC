using System.Collections.Generic;

namespace Features.Abilities
{
    internal interface IAbilitiesModel
    {
        public List<IAbility> Abilities { get; }

        public IReadOnlyList<IAbility> GetEquippedItems();
        public void EquipItem(IAbility ability);
    }
}