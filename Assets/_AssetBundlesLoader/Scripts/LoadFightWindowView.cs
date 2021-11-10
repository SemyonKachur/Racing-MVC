using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace AssetBundles
{
    [Serializable]
    internal class LoadFightWindowView
    {
        private Transform _placeForUI;
        [field: SerializeField] public AssetReference LoadPrefab;

        private List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
        new List<AsyncOperationHandle<GameObject>>();
        
        public LoadFightWindowView(Transform placeForUI, AssetReference prefab)
        {
            _placeForUI = placeForUI;
            LoadPrefab = prefab;
            
            var addressablePrefab = Addressables.InstantiateAsync(LoadPrefab, _placeForUI);
            _addressablePrefabs.Add(addressablePrefab);
        }

        public async Task<GameObject> GetGameObject()
        {
            GameObject gameObjectPrefab = null;
            foreach (var addressablePrefab in _addressablePrefabs)
            {
                await addressablePrefab.Task;
                if (addressablePrefab.Status == AsyncOperationStatus.Succeeded)
                {
                    gameObjectPrefab = addressablePrefab.Result;
                }
            }
            return gameObjectPrefab;
        }

        private void OnDestroy()
        {
            foreach (var addressablePrefab in _addressablePrefabs) 
                Addressables.ReleaseInstance(addressablePrefab);
            _addressablePrefabs.Clear();
        }
    }
}
