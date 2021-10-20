using Game.Car;
using Tool;
using UnityEngine;

namespace Profile
{
    internal class ProfilePlayer
    {
        [field: SerializeField] public SubscriptionProperty<GameState> CurrentState;
        [field: SerializeField] public CarModel CurrentCar;
        [field: SerializeField] public Transport Transport;
        [field: SerializeField] public int Gold;
        [field: SerializeField] public int Oil;


        public ProfilePlayer(float speedCar, GameState initialState,Transport transport,int gold, int oil) : this(speedCar)
        {
            CurrentState.Value = initialState;
            Transport = transport;
            Gold = gold;
            Oil = oil;
        }
        
        public ProfilePlayer(float speedCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar);
        }

        public void AddGold(int ammountOfGold)
        {
            Gold += ammountOfGold;
        }

        public void AddOil(int ammountOfOil)
        {
            Oil += ammountOfOil;
        }
        
    }
}
