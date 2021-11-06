using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AI
{
    internal class PlayerGameUIView : MonoBehaviour
    {
        [field: SerializeField] public Button FightButton;

        public void Init(UnityAction fight)
        {
            FightButton.onClick.AddListener(fight);
        }
    }
}