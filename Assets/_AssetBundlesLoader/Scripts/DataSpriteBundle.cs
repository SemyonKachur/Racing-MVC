using System;
using UnityEngine;
using UnityEngine.UI;

namespace AssetBundles
{
    [Serializable]
    internal class DataSpriteBundle
    {
        [field: SerializeField] public string NameAssetBundle;
        [field: SerializeField] public Image Image;
    }
}