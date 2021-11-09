using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shed
{
    internal class ShedView : MonoBehaviour
    {
        [field: SerializeField] public Button ButtonApply;
        [field: SerializeField] public Button ButtonBack;


        public void Init(UnityAction apply, UnityAction back)
        {
            ButtonApply.onClick.AddListener(apply);
            ButtonBack.onClick.AddListener(back);
        }

        private void OnDestroy()
        {
            ButtonApply.onClick.RemoveAllListeners();
            ButtonBack.onClick.RemoveAllListeners();
        }
    }
}
