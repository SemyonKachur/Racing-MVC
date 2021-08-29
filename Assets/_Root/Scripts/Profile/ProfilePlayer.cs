using Game.Car;
using Tool;
using Configs;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;
        public readonly Transport Transport;


        public ProfilePlayer(float speedCar, GameState initialState,Transport transport) : this(speedCar)
        {
            CurrentState.Value = initialState;
            Transport = transport;
        }
        
        public ProfilePlayer(float speedCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar);
        }
        
    }
}
