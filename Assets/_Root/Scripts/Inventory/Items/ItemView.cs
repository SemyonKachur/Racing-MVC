using Tool;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    internal class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private CustomText _text;


        public void Init(IItem item)
        {
            _text.Text = item.Info.Title;
            _icon.sprite = item.Info.Icon;
        }
    }
}
