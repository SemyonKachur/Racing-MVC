using Configs;
using Profile;
using Services.Analytics;
using Tool;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;

    private readonly ResourcePath _resourcePath = new ResourcePath("ScriptableObjects/ProfilePlayer");
    private PlayerStatsConfig _playerStats;
    private MainController _mainController;
    [SerializeField] AnalyticsManager _analytics;

    private void Awake()
    {
        _playerStats = ResourcesLoader.LoadPlayerStats(_resourcePath);
        var profilePlayer = new ProfilePlayer(_playerStats.Speed,_playerStats.GameState,_playerStats.Transport);
        _mainController = new MainController(_placeForUi, profilePlayer);
        _analytics.SendMainMenuOpened();
    }

    protected void OnDestroy()
    {
        _mainController.Dispose();
    }
}
