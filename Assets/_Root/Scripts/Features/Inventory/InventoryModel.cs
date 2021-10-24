using System.Collections.Generic;

namespace Inventory
{
    internal class InventoryModel : IInventoryModel
    {
        private readonly List<IItem> _items = new List<IItem>();
        
        public IReadOnlyList<IItem> GetEquippedItems() => _items;
        public List<IItem> Items => _items;

        public void EquipItem(IItem item)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
        }

        public void UnequipItem(IItem item)
        {
            if (!_items.Contains(item))
                return;

            _items.Remove(item);
        }
    }

    namespace Inventory
    {
    }
}
