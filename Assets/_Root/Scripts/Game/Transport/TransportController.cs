using System;
using Features.Abilities;
using Game.Boat;
using Game.Car;
using UnityEngine;

namespace Game.Transport
{
    internal class TransportController : BaseController, IAbilityActivator
    {
        private TransportType _transportType;
        private CarController _carController;
        private BoatController _boatController;
        private GameObject _transportView;

        public TransportController(TransportType type)
        {
            switch (type)
            {
                case TransportType.Car:
                    _carController = new CarController();
                    _transportView = _carController.GetViewObject();
                    AddController(_carController);
                    break;
                case TransportType.Boat:
                    _boatController = new BoatController();
                    _transportView = _boatController.GetViewObject();
                    AddController(_boatController);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        protected TransportController()
        {
        }

        public GameObject GetViewObject()
        {
            return _transportView;
        }
    }
}