using UnityEngine;

namespace Inventory
{
    internal readonly struct ItemInfo
    {
        public readonly string Title;
        public readonly Sprite Icon;


        public ItemInfo(string title, Sprite icon)
        {
            Title = title;
            Icon = icon;
        }
    }
}
