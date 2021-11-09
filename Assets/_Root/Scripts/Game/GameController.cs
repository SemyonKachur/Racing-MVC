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
        private readonly AnalyticsManager _analytics = AnalyticsManager.GetAnalytics();
        public TransportController Transport { get; }
        private readonly ProfilePlayer _profilePlayer;
        public GameController(ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _analytics.GameStarted();
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentTransport);
            AddController(inputGameController);
            
            Transport = CreateController();
        }

        private TransportController CreateController()
        {
            TransportController transportController = new TransportController(_profilePlayer.CurrentTransport.Type);
            AddController(transportController);
            return transportController;
        }
    }
}
