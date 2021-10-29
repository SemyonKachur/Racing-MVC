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
        private ProfilePlayer _profilePlayer;
        public GameController(ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            Analytics.GameStarted();
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentTransport);
            AddController(inputGameController);
            
            _transport = CreateController();
        }

        private TransportController CreateController()
        {
            TransportController transportController;
            switch (_profilePlayer.CurrentTransport.Type)
            {
                case TransportType.Car:
                    transportController = new CarController();
                    break;
                case TransportType.Boat:
                    transportController = new BoatController();
                    break;
                default:
                    transportController = new TransportController(TransportType.Car);
                    break;
            }
            AddController(transportController);
            return transportController;
        }
    }
}
