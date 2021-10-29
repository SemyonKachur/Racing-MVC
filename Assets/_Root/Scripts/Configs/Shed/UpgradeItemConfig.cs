using Configs.Inventory;
using UnityEngine;

namespace Configs.Shed
{
    [CreateAssetMenu(fileName = nameof(UpgradeItemConfig), menuName = "Configs/Shed/" + nameof(UpgradeItemConfig))]
    internal class UpgradeItemConfig : ScriptableObject
    {
        [field: SerializeField] public ItemConfig ItemConfig { get; private set; }
        [field: SerializeField] public UpgradeType Type { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public int Id => ItemConfig.Id;
    }
}
