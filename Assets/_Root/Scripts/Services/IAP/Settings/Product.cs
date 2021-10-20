using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace Services.IAP
{
    [Serializable]
    internal struct Product
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public ProductType ProductType { get; private set; }
    }
}
