using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewardedAds;
        [SerializeField] private Button _buttonForBuying;


        public void Init(UnityAction startGame, UnityAction settings, UnityAction rewardedAds,UnityAction buyItem)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonRewardedAds.onClick.AddListener(rewardedAds);
            _buttonForBuying.onClick.AddListener(buyItem);
         }

        public void OnDestroy() =>
            _buttonStart.onClick.RemoveAllListeners();
    }
}
