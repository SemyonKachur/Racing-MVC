using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shed
{
    internal class ShedView : MonoBehaviour
    {
        [SerializeField] private Button _buttonApply;
        [SerializeField] private Button _buttonBack;


        public void Init(UnityAction apply, UnityAction back)
        {
            _buttonApply.onClick.AddListener(apply);
            _buttonBack.onClick.AddListener(back);
        }

        private void OnDestroy()
        {
            _buttonApply.onClick.RemoveAllListeners();
            _buttonBack.onClick.RemoveAllListeners();
        }
    }
}
