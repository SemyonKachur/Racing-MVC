

using Configs.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability item", menuName = "Ability item", order = 0)]
public class AbilityItemConfig : ScriptableObject
{
    public ItemConfig itemConfig;
    public GameObject view;
    public AbilityType type;
    public float value;
    public int Id => itemConfig.Id;
}