using AssetBundles;
using DoTweens;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AI
{
    internal class FightController : BaseController
    {
        private ResourcePath _path = new ResourcePath("Prefabs/Fight Window View");
        
        private readonly ProfilePlayer _profilePlayer;
        private readonly Transform _placeForUI;
        private readonly FightWindowView _view;
        private readonly FightModel _fightModel;
        private readonly FightAnimations _fightAnimations;
        private readonly AssetReference _reference;
        private readonly LoadFightWindowView _fightWindowView;
        

        public FightController(ProfilePlayer profilePlayer, Transform placeForUI, AssetReference reference)
        {
            _profilePlayer = profilePlayer;
            _placeForUI = placeForUI;
            _reference = reference;
            _fightModel = new FightModel();

            _fightWindowView = new LoadFightWindowView(_placeForUI, _reference);
            var go = _fightWindowView.GetGameObject();
            var asd = go.Result;
            _view = asd.GetComponent<FightWindowView>();
            
            // _view = LoadView(_placeForUI);
            _view.Init(ChangeMoney, ChangeHealth, ChangePower, Fight, SkipFight);
            RefreshDataUI();

            _fightAnimations = new FightAnimations(_view);
            _fightAnimations.Winner += VictoryAnimation;
        }
        
        private FightWindowView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_path);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FightWindowView>();
        }
        
        private void SkipFight()
        {
            Debug.Log("You skipped the fight");
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void Fight()
        {
            _fightAnimations.FightAnimation();
        }

        private void VictoryAnimation()
        {
            if(_fightModel.Power.CountPower >= _fightModel.Enemy.Power)
                _fightAnimations.PlayerWinAnimation();
            else
                _fightAnimations.PlayerLooseAnimation();
        }

        private void ChangeMoney(bool isAddMoney)
        {
            _fightModel.ChangeMoney(isAddMoney);
            RefreshDataUI();
        }
    
        private void ChangeHealth(bool isAddHealth)
        {
            _fightModel.ChangeHealth(isAddHealth);
            RefreshDataUI();
        }

        private void ChangePower(bool isAddPower)
        {
            _fightModel.ChangePower(isAddPower);
            RefreshDataUI();
        }

        private void RefreshDataUI()
        {
            _view.CountMoneyText.text = $"Player Money: {_fightModel.Money.CountMoney}";
            _view.CountHealthText.text = $"Player Health: {_fightModel.Health.CountHealth}";
            _view.CountPowerText.text = $"Player Power: {_fightModel.Power.CountPower}";
            
            _fightModel.Enemy.Update(_fightModel.DataPlayer);
            _view.CountPowerEnemyText.text = $"Enemy Power: {_fightModel.Enemy.Power}";
            _view.CountCrimeEnemyText.text = $"Enemy Crime: {_fightModel.Enemy.Crime}";

            var PowerToSkipFight = 5;
            if (_fightModel.Enemy.Crime < 5 || _fightModel.Power.CountPower > _fightModel.Enemy.Power+PowerToSkipFight)
                _view.SkipFight.gameObject.SetActive(true);
            else 
                _view.SkipFight.gameObject.SetActive(false);
        }
    }
}