using UnityEngine;
using Game.Car;

namespace Profile
{
    [CreateAssetMenu(fileName = "ProfilePlayerCar", menuName = "Data/ProfilePlayer/Car")]

    internal class ProfilePlayerSO : ScriptableObject
    {
        public enum Transport
        {
            car,
            boat
        }

        [field: SerializeField] public  GameState gameState { get; private set; } = GameState.Start;
        [field: SerializeField] public Transport transport { get; private set; } = Transport.boat;
        [field: SerializeField] public static float CarSpeed { get; private set; } = 15.0f;
        internal CarModel _car = new CarModel(CarSpeed);            

    }
}
