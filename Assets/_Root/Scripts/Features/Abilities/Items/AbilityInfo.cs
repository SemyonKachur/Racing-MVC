using UnityEngine;

namespace Features.Abilities.Items
{
    internal class AbilityInfo
    {
        public readonly string Title;
        public readonly Sprite Icon;

        public AbilityInfo(string title, Sprite icon)
        {
            Title = title;
            Icon = icon;
        }
    }
}