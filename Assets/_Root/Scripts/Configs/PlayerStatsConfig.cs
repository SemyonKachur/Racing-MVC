using UnityEngine;
using Profile;

namespace Configs
{
    [CreateAssetMenu(fileName = "ProfilePlayer", menuName = "Data/ProfilePlayer")]
    internal class PlayerStatsConfig : ScriptableObject
    {
        [field: SerializeField] public GameState GameState = GameState.Start;
        [field: SerializeField] public Transport Transport = Transport.Boat;
        [field: SerializeField] public float Speed = 15.0f;
        [field: SerializeField] public int Gold = 0;
        [field: SerializeField] public int Oil = 0;
    }
}
