namespace Shed
{
    internal interface IUpgradableTransport
    {
        float Speed { get; set; }
        float JumpHeight { get; set; }
        int GunDamage { get; set; }
        int Health { get; set; }

        void Restore();
    }
}
