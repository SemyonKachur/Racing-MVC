using System;
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

    public void SetData(Reward reward, int countDay, bool isSelected, PlayerRewardType rewardType)
    {
        _iconCurrency.sprite = reward.Sprite;
        _countReward.text = reward.CountCurrency.ToString();
        _selectedBackground.gameObject.SetActive(isSelected);
        switch (rewardType)
        {
            case PlayerRewardType.Daily:
                _textDay.text = $"Day {countDay}";
                break;
            case PlayerRewardType.Weekly:
                _textDay.text = $"Week {countDay}";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(rewardType), rewardType, null);
        }
    }
}
