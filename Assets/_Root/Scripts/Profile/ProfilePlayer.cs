using Game;
using Game.Transport;
using Inventory;
using Tool;
using UnityEngine;

namespace Profile
{
    internal class ProfilePlayer
    {
        [field: SerializeField] public SubscriptionProperty<GameState> CurrentState;
        [field: SerializeField] public TransportModel CurrentTransport;
        [field: SerializeField] public InventoryModel Inventory;
        [field: SerializeField] public int Gold;
        [field: SerializeField] public int Oil;


        public ProfilePlayer(float transportSpeed,TransportType transportType, GameState initialState,int gold, int oil)
        {
            CurrentState = new SubscriptionProperty<GameState>(initialState);;
            CurrentTransport = new TransportModel(transportSpeed, transportType);
            Inventory = new InventoryModel();
            Gold = gold;
            Oil = oil;
        }

        public void AddGold(int ammountOfGold)
        {
            Gold += ammountOfGold;
        }

        public void AddOil(int ammountOfOil)
        {
            Oil += ammountOfOil;
        }
        
    }
}
