using System.Collections.Generic;
using Configs.Inventory;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(InventoryModelConfig), menuName = "Configs/" + nameof(InventoryModelConfig))]
    internal class InventoryModelConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _items;

        public IReadOnlyList<ItemConfig> Items => _items;
    }
}