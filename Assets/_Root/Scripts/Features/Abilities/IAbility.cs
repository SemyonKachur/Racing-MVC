using Features.Abilities.Items;

namespace Features.Abilities
{
    internal interface IAbility
    {
        void Apply(IAbilityActivator activator);
        int Id { get; }
        AbilityInfo Info { get; }
    }
}