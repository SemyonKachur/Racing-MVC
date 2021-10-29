namespace Shed
{
    internal interface IUpgradeTransportHandler
    {
        IUpgradableTransport Upgrade(IUpgradableTransport upgradableTransport);
    }

}
