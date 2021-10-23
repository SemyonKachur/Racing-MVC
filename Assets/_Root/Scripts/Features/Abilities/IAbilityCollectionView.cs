using System;
using System.Collections.Generic;
using Inventory;

namespace Features.Abilities
{
    internal interface IAbilityCollectionView
    {
        event EventHandler<IItem> UseRequested;
        void Display(IReadOnlyList<IItem> abilityItems);
    }
}