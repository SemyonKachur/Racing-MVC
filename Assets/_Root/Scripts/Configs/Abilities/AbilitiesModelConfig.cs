using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AbilitiesModelConfig), menuName = "Configs/" + nameof(AbilitiesModelConfig))]
internal class AbilitiesModelConfig : ScriptableObject
{
    [field: SerializeField] private AbilityItemConfig[] Abilities;
    public IReadOnlyList<AbilityItemConfig> AbilityList => Abilities;
}