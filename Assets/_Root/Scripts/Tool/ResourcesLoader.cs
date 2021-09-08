using UnityEngine;
using Configs;
using Services.Ads.UnityAds;

namespace Tool
{
    internal static class ResourcesLoader
    {
        public static Sprite LoadSprite(ResourcePath path) =>
            Resources.Load<Sprite>(path.PathResource);

        public static GameObject LoadPrefab(ResourcePath path) =>
            Resources.Load<GameObject>(path.PathResource);

        public static PlayerStatsConfig LoadPlayerStats(ResourcePath path) =>
            Resources.Load<PlayerStatsConfig>(path.PathResource);

        public static UnityAdsSettings LoadAdsSettings(ResourcePath path) =>
            Resources.Load<UnityAdsSettings>(path.PathResource);
    }
}
