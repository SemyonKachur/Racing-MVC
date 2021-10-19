using UnityEngine;

namespace Configs.Inventory
{
    [CreateAssetMenu(fileName = nameof(ItemConfig), menuName = "Configs/Inventory/" + nameof(ItemConfig))]
    internal class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
