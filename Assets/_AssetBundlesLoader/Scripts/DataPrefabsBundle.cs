using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AssetBundles
{
    internal class DataPrefabsBundle
    {
        [field: SerializeField] public AssetReferenceGameObject _reference;
        [field: SerializeField] public string NameAssetBundle;
        [field: SerializeField] public GameObject Prefab;

        public DataPrefabsBundle()
        {
            
        }
    }
}