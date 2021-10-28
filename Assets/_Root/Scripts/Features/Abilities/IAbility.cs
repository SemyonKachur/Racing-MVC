using Inventory;

namespace Features.Abilities
{
    internal interface IAbility : IItem
    {
        void Apply(IAbilityActivator activator);
    }
}