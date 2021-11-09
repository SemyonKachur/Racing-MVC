using System.Collections.Generic;
using AI;
using Configs.Shed;
using Features.Abilities;
using Ui;
using Game;
using Profile;
using Rewards;
using Shed;
using UnityEngine;

internal class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private ShedController _shedController;
    private GameController _gameController;
    private SettingsController _settingsController;
    private AbilitiesController _abilitiesController;
    private RewardsController _rewardsController;
    private PlayerUIController _playerUIController;
    private FightController _fightController;
    
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItemConfigs;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, IReadOnlyList<UpgradeItemConfig> upgradeItemConfigs)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _upgradeItemConfigs = upgradeItemConfigs;
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    protected override void OnDispose()
    {
        DisposeControllers();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                DisposeControllers();
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                DisposeControllers();
                _gameController = new GameController(_profilePlayer);
                _abilitiesController = new AbilitiesController(_gameController.Transport,
                    _profilePlayer.Abilities,
                    _profilePlayer.Abilities,
                    _placeForUi);
                _playerUIController = new PlayerUIController(_profilePlayer, _placeForUi);
                break;
            case GameState.Fight:
                DisposeControllers();
                _fightController = new FightController(_profilePlayer, _placeForUi);
                break;
            case GameState.Settings:
                DisposeControllers();
                _settingsController = new SettingsController(_placeForUi,_profilePlayer);
                break;
            case GameState.Shed:
                DisposeControllers();
                _shedController = new ShedController(_placeForUi, _profilePlayer, _upgradeItemConfigs);
                break;
            case GameState.Rewards:
                DisposeControllers();
                _rewardsController = new RewardsController(_placeForUi,_profilePlayer);
                break;
            case GameState.Quit:
                DisposeControllers();
                Application.Quit();
                break;
            default:
                DisposeControllers();
                break;
        }
    }
    private void DisposeControllers()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsController?.Dispose();
        _shedController?.Dispose();
        _abilitiesController?.Dispose();
        _rewardsController?.Dispose();
        _playerUIController?.Dispose();
        _fightController?.Dispose();
    }
}
