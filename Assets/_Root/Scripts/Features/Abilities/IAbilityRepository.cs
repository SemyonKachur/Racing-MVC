using System.Collections.Generic;

namespace Features.Abilities
{
    public interface IAbilityRepository
    {
        IReadOnlyDictionary<int, IAbility> AbilityMapByItemId { get; }   
    }
}