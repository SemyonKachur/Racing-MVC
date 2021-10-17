using Services.Ads.UnityAds.Settings;
using Tool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal class UnityAdsService : IUnityAdsInitializationListener, IAdsService
    {
        [Header("Components")]
        public static UnityAdsService Service;
        private UnityAdsSettings _settings;
        private ResourcePath _resourcePath = new ResourcePath("ScriptableObjects/UnityAdsSettings");

        [field: Header("Events")] public UnityEvent Initialized { get;} = new UnityEvent();

        public IAdsPlayer InterstitialPlayer { get; private set; }
        public IAdsPlayer RewardedPlayer { get; private set; }
        public IAdsPlayer BannerPlayer { get; private set; }


        public UnityAdsService()
        {
            Service = this;
            _settings = ResourcesLoader.LoadAdsSettings(_resourcePath);
            InitializeAds();
            InitializePlayers();
        }

        public static UnityAdsService GetUnityAds()
        {
            if (Service == null) Service = new UnityAdsService();
            return Service;
        }

        private void InitializeAds() =>
            Advertisement.Initialize(
                _settings.GameId,
                _settings.TestMode,
                _settings.EnablePerPlacementMode,
                this);

        private void InitializePlayers()
        {
            InterstitialPlayer = CreateInterstitial();
            RewardedPlayer = CreateRewarded();
            BannerPlayer = CreateBanner();
        }


        private IAdsPlayer CreateInterstitial() =>
            _settings.Interstitial.Enabled
                ? new InterstitialPlayer(_settings.Interstitial.Id)
                : (IAdsPlayer)new EmptyPlayer("");

        private IAdsPlayer CreateRewarded() =>
            _settings.Rewarded.Enabled
               ? new RewardedPlayer(_settings.Rewarded.Id) :
                (IAdsPlayer) new EmptyPlayer("");

        private IAdsPlayer CreateBanner() =>
            new EmptyPlayer("");


        public void OnInitializationComplete()
        {
            Log("Initialization complete.");
            Initialized?.Invoke();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message) =>
            Error($"Initialization Failed: {error.ToString()} - {message}");


        private void Log(string message) => Debug.Log(WrapMessage(message));
        private void Error(string message) => Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
