namespace Shed
{
    internal class SpeedUpgradeTransportHandler : IUpgradeTransportHandler
    {
        private readonly float _speed;


        public SpeedUpgradeTransportHandler(float speed) =>
            _speed = speed;


        public IUpgradableTransport Upgrade(IUpgradableTransport upgradableTransport)
        {
            upgradableTransport.Speed += _speed;
            return upgradableTransport;
        }
    }
}
