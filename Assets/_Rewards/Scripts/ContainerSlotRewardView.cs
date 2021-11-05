using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerSlotRewardView : MonoBehaviour
{
    [SerializeField]
    private Image _selectedBackground;

    [SerializeField]
    private Image _iconCurrency;

    [SerializeField]
    private TMP_Text _textDay;

    [SerializeField]
    private TMP_Text _countReward;

    public void SetData(Reward reward, int countDay, bool isSelected)
    {
        _iconCurrency.sprite = reward.Sprite;
        _textDay.text = $"Day {countDay}";
        _countReward.text = reward.CountCurrency.ToString();
        _selectedBackground.gameObject.SetActive(isSelected);
    }
}
