using Config;
using Configs.Inventory;
using Inventory;

internal class InventoryInitializer
{
    private readonly InventoryModelConfig _inventoryModelConfig;


    public InventoryInitializer(InventoryModelConfig inventoryModelConfig) =>
        _inventoryModelConfig = inventoryModelConfig;


    public void InitializeModel(InventoryModel inventoryModel)
    {
        foreach (ItemConfig itemConfig in _inventoryModelConfig.Items)
        {
            var item = CreateItem(itemConfig);
            inventoryModel.EquipItem(item);
        }
    }

    private IItem CreateItem(ItemConfig itemConfig)
    {
        var itemInfo = new ItemInfo(itemConfig.Title, itemConfig.Icon);
        return new Item(itemConfig.Id, itemInfo);
    }
}