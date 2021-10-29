using Features.Abilities.Items;

namespace Inventory
{
    internal interface IItem
    {
        int Id { get; }
        ItemInfo Info { get; }
    }

    internal class Item : IItem
    {
        public int Id { get; private set; }
        public ItemInfo Info { get; private set; }


        public Item(int id, ItemInfo info)
        {
            Id = id;
            Info = info;
        }
    }
}
