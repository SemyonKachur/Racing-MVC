using System;
using System.Collections.Generic;

namespace Features.Abilities
{
    internal interface IAbilityCollectionView
    {
        event EventHandler<IAbility> UseRequested;
        void Display(IReadOnlyList<IAbility> abilityItems);
    }
}