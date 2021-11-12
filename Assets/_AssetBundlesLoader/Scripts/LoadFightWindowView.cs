using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AssetBundles
{
    internal class LoadFightWindowView
    {
        [SerializeField] private Transform _placeForUI;
        AssetReference _reference = new AssetReference("Assets/_AI/Fight Window View.prefab");
        private GameObject prefab { get; set; }
       
        private List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
        new List<AsyncOperationHandle<GameObject>>();
        public event Action<GameObject> action;
        
        public LoadFightWindowView()
        {
            var _addressablePrefab = Addressables.InstantiateAsync(_reference, _placeForUI);
            _addressablePrefabs.Add(_addressablePrefab);
            
            foreach (var addressablePrefabs in _addressablePrefabs)
            {
                addressablePrefabs.Completed += Complete;
            }
        }
        private void Complete(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                prefab = obj.Result;
                action?.Invoke(prefab);
            }
        }

        private void OnDestroy()
        {
            foreach (var addressablePrefab in _addressablePrefabs) 
                Addressables.ReleaseInstance(addressablePrefab);
            _addressablePrefabs.Clear();
        }
    }
}
