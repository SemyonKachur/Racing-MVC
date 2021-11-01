using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [field:SerializeField] public Button ButtonStart;
        [field:SerializeField] public Button ButtonSettings;
        [field:SerializeField] public Button ButtonRewardedAds;
        [field:SerializeField] public Button ButtonForBuyingGold;
        [field:SerializeField] public Button ButtonForBuyingOil;
        [field: SerializeField] public Button ButtonRewards;
        [field:SerializeField] public Button ButtonShed;
        [field:SerializeField] public Text GoldCount;
        [field:SerializeField] public Text OilCount;


        public void Init(UnityAction startGame, UnityAction settings, UnityAction rewardedAds,
            UnityAction buyGold, UnityAction buyOil, UnityAction shed,UnityAction rewards, int gold, int oil)
        {
            ButtonStart.onClick.AddListener(startGame);
            ButtonSettings.onClick.AddListener(settings);
            ButtonRewardedAds.onClick.AddListener(rewardedAds);
            ButtonForBuyingGold.onClick.AddListener(buyGold);
            ButtonForBuyingOil.onClick.AddListener(buyOil);
            ButtonShed.onClick.AddListener(shed);
            ButtonRewards.onClick.AddListener(rewards);
            GoldCount.text = gold.ToString();
            OilCount.text = oil.ToString();
        }

        public void OnDestroy() =>
            ButtonStart.onClick.RemoveAllListeners();
    }
}
