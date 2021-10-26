using Features.Abilities;
using Game.Boat;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Game.Transport;
using Profile;
using Services.Analytics;
using Tool;

namespace Game
{
    internal class GameController : BaseController
    {
        private AnalyticsManager Analytics = AnalyticsManager.GetAnalytics();
        public TransportController _transport { get; }
        public GameController(ProfilePlayer profilePlayer)
        {
           Analytics.GameStarted();
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentTransport);
            AddController(inputGameController);
            
            _transport = new TransportController(profilePlayer.CurrentTransport.Type);
        }
    }
}
