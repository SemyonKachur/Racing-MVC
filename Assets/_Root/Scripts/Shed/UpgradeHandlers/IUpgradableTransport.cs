namespace Shed
{
    internal interface IUpgradableTransport
    {
        float Speed { get; set; }

        void Restore();
    }
}
