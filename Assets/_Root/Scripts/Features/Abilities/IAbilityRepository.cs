using System.Collections.Generic;

namespace Features.Abilities
{
    internal interface IAbilityRepository
    {
        IReadOnlyDictionary<int, IAbility> AbilityMapByItemId { get; }   
    }
}