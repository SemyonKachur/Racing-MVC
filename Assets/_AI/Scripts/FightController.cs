using AssetBundles;
using DoTweens;
using Profile;
using UnityEngine;

namespace AI
{
    internal class FightController : BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly FightModel _fightModel;
        private readonly LoadFightWindowView _fightWindowView;
        
        private FightWindowView _view;
        private FightAnimations _fightAnimations;

        public FightController(ProfilePlayer profilePlayer, LoadFightWindowView fightWindowView)
        {
            _profilePlayer = profilePlayer;
            _fightWindowView = fightWindowView;
            _fightModel = new FightModel();
            
            _fightWindowView.GetGameObject();
            _fightWindowView.action += ViewInitialize;
        }

        private void ViewInitialize(GameObject gameObject)
        {
            AddGameObject(gameObject);
            _view = gameObject.GetComponent<FightWindowView>();
            
            _view.Init(ChangeMoney, ChangeHealth, ChangePower, Fight, SkipFight);
            RefreshDataUI();

            _fightAnimations = new FightAnimations(_view);
            _fightAnimations.Winner += VictoryAnimation;
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

        protected override void OnDispose()
        {
            _fightWindowView.action -= ViewInitialize;
        }
    }
}