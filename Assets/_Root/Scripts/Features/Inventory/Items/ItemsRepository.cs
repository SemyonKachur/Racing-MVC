using System.Collections.Generic;
using Configs.Inventory;

namespace Inventory
{
    internal interface IItemsRepository : IRepository
    {
        IReadOnlyDictionary<int, IItem> Items { get; }
    }

    internal class ItemsRepository : IItemsRepository
    {
        private readonly Dictionary<int, IItem> _itemsMapById = new Dictionary<int, IItem>();

        public IReadOnlyDictionary<int, IItem> Items => _itemsMapById;


        public ItemsRepository(List<ItemConfig> upgradeItemConfigs) =>
            PopulateItems(ref _itemsMapById, upgradeItemConfigs);

        public void Dispose() =>
            _itemsMapById.Clear();


        private void PopulateItems(ref Dictionary<int, IItem> upgradeHandlersMapByType, List<ItemConfig> configs)
        {
            foreach (var config in configs)
                if (!upgradeHandlersMapByType.ContainsKey(config.Id))
                    upgradeHandlersMapByType.Add(config.Id, CreateItem(config));
        }

        private IItem CreateItem(ItemConfig config)
        {
            var itemInfo = new ItemInfo(config.Title, config.Icon);
            return new Item(config.Id, itemInfo);
        }
    }

}
