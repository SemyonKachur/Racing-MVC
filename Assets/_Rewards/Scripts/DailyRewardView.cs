using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardView : MonoBehaviour
{
    private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
    private const string TimeGetRewardKey = nameof(TimeGetRewardKey);
    [Header("Timers")]
    [field: SerializeField] public float TimeCooldown = 86400;
    [field: SerializeField] public float TimeDeadline = 172800;
    [Header("Lists of rewards")]
    [field: SerializeField] public List<Reward> DailyRewards;
    [field: SerializeField] public List<Reward> WeeklyRewards;

    [Header("Timer's progress bar")]
    [field: SerializeField] public TMP_Text TimerNewDailyReward;
    [field: SerializeField] public Image TimerDailyRewardBar;
    [field: SerializeField] public TMP_Text TimerNewWeeklyReward;
    [field: SerializeField] public Image TimerWeeklyRewardBar;
    [Header("Conteiners")]
    [field: SerializeField] public Transform DailyRootSlotsReward;
    [field: SerializeField] public ContainerSlotRewardView DailyContainerSlotRewardView;
    [field: SerializeField] public Transform WeeklyRootSlotsReward;
    [field: SerializeField] public ContainerSlotRewardView WeeklyContainerSlotRewardView;
    [Header("Buttons")]
    [field: SerializeField] public Button GetRewardButton;
    [field: SerializeField] public Button ResetButton;
    [field: SerializeField] public Button BackButton;
    
    public int CurrentDailySlotInActive
    {
        get => PlayerPrefs.GetInt(CurrentSlotInActiveKey, 0);
        set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
    }
    public int CurrentWeeklySlotInActive
    {
        get => PlayerPrefs.GetInt(CurrentSlotInActiveKey, 0);
        set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
    }

    public DateTime? TimeGetReward
    {
        get
        {
            var data = PlayerPrefs.GetString(TimeGetRewardKey, null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
            else
                PlayerPrefs.DeleteKey(TimeGetRewardKey);
        }
    }

    private void OnDestroy()
    {
        GetRewardButton.onClick.RemoveAllListeners();
        ResetButton.onClick.RemoveAllListeners();
        BackButton.onClick.RemoveAllListeners();
    }
}
