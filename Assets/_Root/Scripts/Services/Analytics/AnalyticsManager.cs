using Services.Analytics.UnityAnalytics;
using UnityEngine;

namespace Services.Analytics
{
    internal class AnalyticsManager
    {
        private static AnalyticsManager Analytics;
        private IAnalyticsService[] _services;


        public AnalyticsManager()
        {
            Analytics = this;
            InitializeServices();
        }

        public static AnalyticsManager GetAnalytics()
        {
            if (Analytics == null) Analytics = new AnalyticsManager();
            return Analytics;
        }
    
        private void InitializeServices()
        {
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }
            
        
        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");

        public void GameStarted()
        {
            SendEvent("GameStarted");
            Debug.Log("GameStarted");
        } 
            
        private void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }
    }
}
