using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class SettingsView : MonoBehaviour
    {
        [field: SerializeField] public Button ButtonBack;


        public void Init(UnityAction backToMainMenu) =>
            ButtonBack.onClick.AddListener(backToMainMenu);

        public void OnDestroy() =>
            ButtonBack.onClick.RemoveAllListeners();
    }
}