using System.Collections.Generic;
using UnityEngine;

namespace Configs.Shed
{
    [CreateAssetMenu(fileName = nameof(UpgradeItemConfigDataSource), menuName = "Configs/Shed/" + nameof(UpgradeItemConfigDataSource))]
    internal class UpgradeItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private UpgradeItemConfig[] _itemConfigs;

        public IReadOnlyList<UpgradeItemConfig> ItemConfigs => _itemConfigs;
    }

}
