using Shed;

namespace Game.Transport
{
    internal class TransportModel : IUpgradableTransport
    {
        public float Speed { get; set; }
        public float JumpHeight { get; set; }
        public int GunDamage { get; set; }
        public int Health { get; set; }
        public TransportType Type { get; }

        private readonly float _defaultSpeed;
        private readonly float _defaultJumpHeight;
        private readonly int _defaultGunDamage;
        private readonly int _defaultHealth;


        public TransportModel(float speed, TransportType type)
        {
            Speed = speed;
            Type = type;
            _defaultSpeed = speed;
        }

        public void Restore()
        {
            Speed = _defaultSpeed;
            JumpHeight = _defaultJumpHeight;
            GunDamage = _defaultGunDamage;
            Health = _defaultHealth;
        }
    }
}
