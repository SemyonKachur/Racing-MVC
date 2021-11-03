using System;
using System.Collections;
using System.Collections.Generic;
using Profile;
using UnityEngine;

internal class DailyRewardController : BaseController, IDisposable
{
    private DailyRewardView _dailyRewardView;
    private List<ContainerSlotRewardView> _slots;
    public event Action _mainMenu;
  
    private bool _isGetReward;

    public DailyRewardController(DailyRewardView generateLevelView)
    {
       _dailyRewardView = generateLevelView;
    }
  
    public void RefreshView()
    {
       InitSlots();
      
       _dailyRewardView.StartCoroutine(RewardsStateUpdater());
      
       RefreshUi();
       SubscribeButtons();
    }

    private void InitSlots()
    {
       _slots = new List<ContainerSlotRewardView>();

       for (var i = 0; i < _dailyRewardView.Rewards.Count; i++)
       {
           var instanceSlot = GameObject.Instantiate(_dailyRewardView.ContainerSlotRewardView,
               _dailyRewardView.MountRootSlotsReward, false);

           _slots.Add(instanceSlot);
       }
   }

    private IEnumerator RewardsStateUpdater()
    {
       while (true)
       {
           RefreshRewardsState();
           yield return new WaitForSeconds(1);
       }
    }

    private void RefreshRewardsState()
    {
       _isGetReward = true;

       if (_dailyRewardView.TimeGetReward.HasValue)
       {
           var timeSpan = DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;

           if (timeSpan.Seconds > _dailyRewardView.TimeDeadline)
           {
               _dailyRewardView.TimeGetReward = null;
               _dailyRewardView.CurrentSlotInActive = 0;
           }
           else if (timeSpan.Seconds < _dailyRewardView.TimeCooldown)
           {
               _isGetReward = false;
           }
       }

       RefreshUi();
   }

    private void RefreshUi()
    {
       _dailyRewardView.GetRewardButton.interactable = _isGetReward;

       if (_isGetReward)
       {
           _dailyRewardView.TimerNewDailyReward.text = "It's time to get reward !";
       }
       else
       {
           if (_dailyRewardView.TimeGetReward != null)
           {
               var nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(_dailyRewardView.TimeCooldown);
               var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
               var sec = currentClaimCooldown.TotalSeconds;
               ToolBarViewTimer((int)sec);
               var timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
               if (currentClaimCooldown <= TimeSpan.Zero)
               {
                   _dailyRewardView.GetRewardButton.interactable = true;
                   currentClaimCooldown = TimeSpan.Zero;
               } 
               _dailyRewardView.TimerNewDailyReward.text = $"Time to get the next reward: {timeGetReward}";
           }
       }

       for (var i = 0; i < _slots.Count; i++)
           _slots[i].SetData(_dailyRewardView.Rewards[i],i + 1, i == _dailyRewardView.CurrentSlotInActive);
    }

    private void ToolBarViewTimer(int sec)
    {
        float kef = 1 / _dailyRewardView.TimeCooldown;
        var progressBar = _dailyRewardView.TimeCooldown - sec;
        _dailyRewardView.TimerDailyRewardBar.fillAmount = kef * progressBar;
    }

   private void SubscribeButtons()
   {
       _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
       _dailyRewardView.ResetButton.onClick.AddListener(ResetTimer);
       _dailyRewardView.BackButton.onClick.AddListener(BackToMainMenu);
   }

   private void ClaimReward()
   {
       if (!_isGetReward)
           return;

       var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive];

       switch (reward.RewardType)
       {
           case RewardType.Wood:
               CurrencyView.Instance.AddWood(reward.CountCurrency);
               break;
           case RewardType.Diamond:
               CurrencyView.Instance.AddDiamond(reward.CountCurrency);
               break;
       }

       _dailyRewardView.TimeGetReward = DateTime.UtcNow;
       _dailyRewardView.CurrentSlotInActive = (_dailyRewardView.CurrentSlotInActive + 1) % _dailyRewardView.Rewards.Count;

       RefreshRewardsState();
   }

   private void ResetTimer()
   {
       PlayerPrefs.DeleteAll();
       CurrencyView.Instance.RefreshText();
   }
   private void BackToMainMenu()
   {
       _mainMenu.Invoke();
   }

   public void Dispose()
   {
   }
}
