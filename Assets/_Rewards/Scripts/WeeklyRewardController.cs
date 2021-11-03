using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    internal class WeeklyRewardController : BaseController, IDisposable
    { 
        private DailyRewardView _dailyRewardView;
        private List<ContainerSlotRewardView> _slots;
  
        private bool _isGetReward;

        public WeeklyRewardController(DailyRewardView generateLevelView)
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

           for (var i = 0; i < _dailyRewardView.WeeklyRewards.Count; i++)
           {
               var instanceSlot = GameObject.Instantiate(_dailyRewardView.WeeklyContainerSlotRewardView,
                   _dailyRewardView.WeeklyRootSlotsReward, false);

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
                   _dailyRewardView.CurrentWeeklySlotInActive = 0;
               }
               else if (timeSpan.Seconds < _dailyRewardView.TimeDeadline)
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
               _dailyRewardView.TimerNewWeeklyReward.text = "It's time to get reward !";
           }
           else
           {
               if (_dailyRewardView.TimeGetReward != null)
               {
                   var nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(_dailyRewardView.TimeDeadline);
                   var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                   var sec = currentClaimCooldown.TotalSeconds;
                   ToolBarViewTimer((int)sec);
                   var timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
                   if (currentClaimCooldown <= TimeSpan.Zero)
                   {
                       _dailyRewardView.GetRewardButton.interactable = true;
                       currentClaimCooldown = TimeSpan.Zero;
                   } 
                   _dailyRewardView.TimerNewWeeklyReward.text = $"Time to get the next reward: {timeGetReward}";
               }
           }

           for (var i = 0; i < _slots.Count; i++)
               _slots[i].SetData(_dailyRewardView.WeeklyRewards[i],i + 1, i == _dailyRewardView.CurrentWeeklySlotInActive);
        }

        private void ToolBarViewTimer(int sec)
        {
            float kef = 1 / _dailyRewardView.TimeDeadline;
            var progressBar = _dailyRewardView.TimeDeadline - sec;
            _dailyRewardView.TimerWeeklyRewardBar.fillAmount = kef * progressBar;
        }

       private void SubscribeButtons()
       {
           _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
           // _dailyRewardView.ResetButton.onClick.AddListener(ResetTimer);
       }

       private void ClaimReward()
       {
           if (!_isGetReward)
               return;

           var reward = _dailyRewardView.WeeklyRewards[_dailyRewardView.CurrentWeeklySlotInActive];

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
           _dailyRewardView.CurrentWeeklySlotInActive = (_dailyRewardView.CurrentWeeklySlotInActive + 1) % _dailyRewardView.WeeklyRewards.Count;

           RefreshRewardsState();
       }

       private void ResetTimer()
       {
           PlayerPrefs.DeleteAll();
           CurrencyView.Instance.RefreshText();
       }

       public void Dispose()
       {
       }
    }
}