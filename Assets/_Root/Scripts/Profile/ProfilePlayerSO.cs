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

        [SerializeField] internal GameState gameState = GameState.Start;
        [SerializeField] public readonly Transport transport = Transport.boat;
        [SerializeField] static float CarSpeed = 15.0f;
        internal CarModel _car = new CarModel(CarSpeed);            

    }
}
