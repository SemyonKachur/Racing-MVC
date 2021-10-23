using System.Collections.Generic;
using Configs.Shed;

namespace Shed
{
    internal class UpgradeHandlersRepository : IRepository
    {
        private readonly Dictionary<int, IUpgradeTransportHandler> _upgradeItemsMapById = new Dictionary<int, IUpgradeTransportHandler>();

        public IReadOnlyDictionary<int, IUpgradeTransportHandler> UpgradeItems => _upgradeItemsMapById;


        public UpgradeHandlersRepository(IReadOnlyList<UpgradeItemConfig> upgradeItemConfigs) =>
            PopulateItems(ref _upgradeItemsMapById, upgradeItemConfigs);


        public void Dispose() =>
            _upgradeItemsMapById.Clear();


        private void PopulateItems(ref Dictionary<int, IUpgradeTransportHandler> upgradeHandlersMapByType, IReadOnlyList<UpgradeItemConfig> configs)
        {
            foreach (var config in configs)
                if (upgradeHandlersMapByType.ContainsKey(config.Id) == false)
                    upgradeHandlersMapByType.Add(config.Id, CreateHandlerByType(config));
        }

        private IUpgradeTransportHandler CreateHandlerByType(UpgradeItemConfig config)
        {
            switch (config.Type)
            {
                case UpgradeType.Speed:
                    return new SpeedUpgradeTransportHandler(config.Value);
                default:
                    return StubUpgradeTransportHandler.Default;
            }
        }
    }
}
