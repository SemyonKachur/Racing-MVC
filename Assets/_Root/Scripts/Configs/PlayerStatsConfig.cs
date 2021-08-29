using UnityEngine;
using Profile;

namespace Configs
{
    [CreateAssetMenu(fileName = "ProfilePlayer", menuName = "Data/ProfilePlayer")]
    internal class PlayerStatsConfig : ScriptableObject
    {
        [SerializeField] private GameState gameState = GameState.Start;
        [SerializeField] private Transport transport = Transport.Boat;
        [SerializeField] private float speed = 15.0f;

        public GameState GameState => gameState;
        public Transport Transport => transport;
        public float Speed => speed;
    }
}
