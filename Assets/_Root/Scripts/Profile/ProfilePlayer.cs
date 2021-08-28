using Game.Car;
using Tool;
using static Profile.ProfilePlayerSO;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;
        public readonly Transport Transport;


        public ProfilePlayer(float speedCar, GameState initialState) : this(speedCar)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(float speedCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar);
        }
        public ProfilePlayer (ProfilePlayerSO profileData)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentState.Value = profileData.gameState;
            CurrentCar = profileData._car;
            Transport = profileData.transport;
        }
    }
}
