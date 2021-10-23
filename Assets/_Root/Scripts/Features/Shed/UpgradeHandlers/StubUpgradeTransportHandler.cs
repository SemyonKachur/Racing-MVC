namespace Shed
{
    internal class StubUpgradeTransportHandler : IUpgradeTransportHandler
    {
        public static readonly IUpgradeTransportHandler Default = new StubUpgradeTransportHandler();

        public IUpgradableTransport Upgrade(IUpgradableTransport upgradableTransport) => upgradableTransport;
    }
}
