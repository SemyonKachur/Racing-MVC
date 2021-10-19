using Configs;
using Config;
using Configs.Shed;
using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using Tool;
using UnityEngine;
using UnityEngine.Purchasing;

internal class EntryPoint : MonoBehaviour
{
    [Header("Configs")]
    private readonly ResourcePath _resourcePath = new ResourcePath("ScriptableObjects/ProfilePlayer");
    [SerializeField] PlayerStatsConfig _playerStats;
    [SerializeField] private InventoryModelConfig _inventoryModelConfig;
    [SerializeField] private UpgradeItemConfigDataSource _upgradeItemConfigDataSource;
    
    [Header("Cpmponents")]
    [SerializeField] private Transform _placeForUi;
    
    [Header("Services")]
    private AnalyticsManager _analytics;
    private UnityAdsService _ads;
    private IAPService _iapService;
    
    private MainController _mainController;

    private void Awake()
    {
        _playerStats = ResourcesLoader.LoadPlayerStats(_resourcePath);
        var profilePlayer = new ProfilePlayer(_playerStats.Speed,_playerStats.GameState,_playerStats.Transport, _playerStats.Gold,_playerStats.Oil);
        _mainController = new MainController(_placeForUi, profilePlayer);
        
        _analytics = new AnalyticsManager();
        _analytics.SendMainMenuOpened();
        _ads = new UnityAdsService();
        _ads.Initialized.AddListener(_ads.InterstitialPlayer.Play);
        _iapService = IAPService.GetIAPService();
    }

    protected void OnDestroy()
    {
        _mainController.Dispose();
        _ads.Initialized.RemoveListener(_ads.InterstitialPlayer.Play);
    }
}
