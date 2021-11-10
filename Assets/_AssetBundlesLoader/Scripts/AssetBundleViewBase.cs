using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AssetBundles
{
    internal class AssetBundleViewBase 
    { 
       private const string UrlAssetBundlePrefabs = "https://drive.google.com/uc?export=download&id=1G9VlCek6esaJoX93A6hwjlH10yW7tJib";

        [SerializeField] private DataSpriteBundle[] _dataSpriteBundles;

        private AssetBundle _spritesAssetBundle;
        private AssetBundle _audioAssetBundle;

        protected IEnumerator DownloadAndSetAssetBundle()
        {
           yield return GetAudioAssetBundle();

           if (_spritesAssetBundle == null || _audioAssetBundle == null)
           {
               Debug.LogError($"AssetBundle {_audioAssetBundle} failed to load");
               yield break;
           }

           SetDownloadAssets();
           yield return null;
        }
      
       private IEnumerator GetAudioAssetBundle()
        {
           var request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundlePrefabs);
          
           yield return request.SendWebRequest();
          
           while (!request.isDone)
               yield return null;

           StateRequest(request, ref _audioAssetBundle);

           yield return null;
        }

        private void StateRequest(UnityWebRequest request, ref AssetBundle assetBundle)
        {
           if (request.error == null)
           {
               assetBundle = DownloadHandlerAssetBundle.GetContent(request);
               Debug.Log("Complete");
           }
           else
           {
               Debug.Log(request.error);
           }
        }
      
        private void SetDownloadAssets()
        {
           foreach (var data in _dataSpriteBundles)
               data.Image.sprite = _spritesAssetBundle.LoadAsset<Sprite>(data.NameAssetBundle);

           // foreach (var data in _dataPrefabBundles)
           // {
           //     // data.AudioSource.clip = _audioAssetBundle.LoadAsset<AudioClip>(data.NameAssetBundle);
           //     // data.AudioSource.Play();
           // }
        }   
    }
}