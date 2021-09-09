using Configs;
using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using Tool;
using UnityEngine;
using UnityEngine.Purchasing;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;

    private readonly ResourcePath _resourcePath = new ResourcePath("ScriptableObjects/ProfilePlayer");
    private PlayerStatsConfig _playerStats;
    private MainController _mainController;
    private AnalyticsManager _analytics;
    private UnityAdsService _ads;
    private IAPService _iapService;


    private void Awake()
    {
        _playerStats = ResourcesLoader.LoadPlayerStats(_resourcePath);
        var profilePlayer = new ProfilePlayer(_playerStats.Speed,_playerStats.GameState,_playerStats.Transport);
        _mainController = new MainController(_placeForUi, profilePlayer);
        _analytics = new AnalyticsManager();
        _analytics.SendMainMenuOpened();
        
        _ads = new UnityAdsService();
        _ads.Initialized.AddListener(OnAdsInitialized);
        
        _iapService = IAPService.GetIAPService();
    }

    protected void OnDestroy()
    {
        _mainController.Dispose();
        _ads.Initialized.RemoveListener(OnAdsInitialized);
    }
    
    private void OnAdsInitialized() => _ads.InterstitialPlayer.Play();
}
