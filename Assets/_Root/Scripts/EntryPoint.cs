using Configs;
using Profile;
using Tool;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;

    private readonly ResourcePath _resourcePath = new ResourcePath("ScriptableObjects/ProfilePlayer");
    private PlayerStatsConfig _playerStats;
    private MainController _mainController;

    private void Awake()
    {
        _playerStats = ResourcesLoader.LoadPlayerStats(_resourcePath);
        var profilePlayer = new ProfilePlayer(_playerStats.Speed,_playerStats.GameState,_playerStats.Transport);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    protected void OnDestroy()
    {
        _mainController.Dispose();
    }
}
