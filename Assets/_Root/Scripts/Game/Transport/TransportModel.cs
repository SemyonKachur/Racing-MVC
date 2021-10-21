using Shed;

namespace Game.Transport
{
    internal class TransportModel : IUpgradableTransport
    {
        public float Speed { get; set; }
        public TransportType Type { get; }

        private readonly float _defaultSpeed;


        public TransportModel(float speed, TransportType type)
        {
            Speed = speed;
            Type = type;
            _defaultSpeed = speed;
        }

        public void Restore()
        {
            Speed = _defaultSpeed;
        }
    }
}
