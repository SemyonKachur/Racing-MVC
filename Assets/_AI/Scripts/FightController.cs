using Profile;
using Tool;
using UnityEngine;

namespace AI
{
    internal class FightController : BaseController
    {
        private ResourcePath _path = new ResourcePath("Prefabs/Fight Window View");
        
        private readonly ProfilePlayer _profilePlayer;
        private readonly Transform _placeForUI;
        private readonly FightWindowView _view;
        private readonly FightModel _fightModel;

        public FightController(ProfilePlayer profilePlayer, Transform placeForUI)
        {
            _profilePlayer = profilePlayer;
            _placeForUI = placeForUI;

            _fightModel = new FightModel();

            _view = LoadView(_placeForUI);
            _view.Init(ChangeMoney, ChangeHealth, ChangePower, Fight, SkipFight);
        }
        
        private FightWindowView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_path);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FightWindowView>();
        }
        
        private void SkipFight()
    {
        Debug.Log("You skipped the fight");
    }

    private void Fight()
    {
        Debug.Log(_fightModel.AllCountPowerPlayer >= _fightModel.Enemy.Power ? "Win" : "Lose");
    }

    private void ChangeMoney(bool isAddMoney)
    {
        _fightModel.ChangeMoney(isAddMoney);
        ChangeDataWindow(_fightModel.AllCountMoneyPlayer, DataType.Money);
    }
    
    private void ChangeHealth(bool isAddHealth)
    {
        _fightModel.ChangeHealth(isAddHealth);
        ChangeDataWindow(_fightModel.AllCountHealthPlayer, DataType.Health);
    }

    private void ChangePower(bool isAddPower)
    {
        _fightModel.ChangePower(isAddPower);
        ChangeDataWindow(_fightModel.AllCountPowerPlayer,DataType.Power);
    }

    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _view.CountMoneyText.text = $"Player Money: {countChangeData}";
                break;

            case DataType.Health:
                _view.CountHealthText.text = $"Player Health: {countChangeData}";
                break;

            case DataType.Power:
                _view.CountPowerText.text = $"Player Power: {countChangeData}";
                break;
        }
            _view.CountPowerEnemyText.text = $"Enemy Power: {_fightModel.Enemy.Power}";
            _view.CountCrimeEnemyText.text = $"Emeny Crime: {_fightModel.Enemy.Crime}";

        var PowerToSkipFight = 5;
        
        if (_fightModel.Enemy.Crime < 5 || _fightModel.Power.CountPower > _fightModel.Enemy.Power+PowerToSkipFight)
            _view.SkipFight.gameObject.SetActive(true);
        else 
            _view.SkipFight.gameObject.SetActive(false);
    }
    }
}