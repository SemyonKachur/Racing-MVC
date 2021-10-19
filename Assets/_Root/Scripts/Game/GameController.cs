using Game.Boat;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Services.Analytics;
using Tool;

namespace Game
{
    internal class GameController : BaseController
    {
        private AnalyticsManager Analytics = AnalyticsManager.GetAnalytics();
        public GameController(ProfilePlayer profilePlayer)
        {
           Analytics.GameStarted();
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentTransport);
            AddController(inputGameController);

            if (profilePlayer.CurrentTransport.Type == TransportType.Car)
            {
                var carController = new CarController();
                AddController(carController);
            }
            else if (profilePlayer.CurrentTransport.Type == TransportType.Boat)
            {
                var boatController = new BoatController();
                AddController(boatController);
            }
        }
    }
}
