using AssetBundles;
using Configs;
using Config;
using Configs.Shed;
using Features.Abilities;
using Inventory;
using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using Tool;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [Header("Configs")]
    private readonly ResourcePath _resourcePath = new ResourcePath("ScriptableObjects/ProfilePlayer");
    [SerializeField] PlayerStatsConfig playerStats;
    [SerializeField] private InventoryModelConfig inventoryModelConfig;
    [SerializeField] private UpgradeItemConfigDataSource upgradeItemConfigDataSource;
    [SerializeField] private AbilitiesModelConfig abilitiesModelConfig;
    
    [Header("Cpmponents")]
    [SerializeField] private Transform placeForUi;
    
    [Header("Services")]
    private AnalyticsManager _analytics;
    private UnityAdsService _ads;

    private MainController _mainController;

    private void Awake()
    {
        playerStats = ResourcesLoader.LoadPlayerStats(_resourcePath);
        var profilePlayer = new ProfilePlayer(playerStats.TransportSpeed,playerStats.TransportType,playerStats.GameState, playerStats.Gold,playerStats.Oil);
        InitializeInventoryModel(inventoryModelConfig, profilePlayer.Inventory);
        InitializeAbilitiesModel(abilitiesModelConfig, profilePlayer.Abilities);
        _mainController = new MainController(placeForUi, profilePlayer,upgradeItemConfigDataSource.ItemConfigs);
        
        _analytics = new AnalyticsManager();
        _analytics.SendMainMenuOpened();
        _ads = new UnityAdsService();
        _ads.Initialized.AddListener(_ads.InterstitialPlayer.Play);
        IAPService.GetIAPService();
    }
    
    private void InitializeInventoryModel(
        InventoryModelConfig inventoryModelConfig,
        InventoryModel inventoryModel)
    {
        var initializer = new InventoryInitializer(inventoryModelConfig);
        initializer.InitializeModel(inventoryModel);
    }

    private void InitializeAbilitiesModel(AbilitiesModelConfig abilitiesModelConfig, AbilitiesModel abilitiesModel)
    {
        var initializer = new AbilitiesInitializer(abilitiesModelConfig);
        initializer.InitializeModel(abilitiesModel);
    }

    protected void OnDestroy()
    {
        _mainController.Dispose();
        _ads.Initialized.RemoveListener(_ads.InterstitialPlayer.Play);
    }
}
