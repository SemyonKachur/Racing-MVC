using System.Collections.Generic;

namespace Inventory
{
    internal interface IInventoryModel
    {
        public List<IItem> Items { get; }

        public IReadOnlyList<IItem> GetEquippedItems();
    }
}