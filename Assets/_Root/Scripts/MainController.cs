using System.Collections.Generic;
using Configs.Shed;
using Features.Abilities;
using Ui;
using Game;
using Profile;
using Services.Ads.UnityAds;
using Shed;
using UnityEngine;

internal class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private ShedController _shedController;
    private GameController _gameController;
    private SettingsController _setingsController;
    private AbilitiesController _abilitiesController;
    
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
                _abilitiesController = new AbilitiesController(_gameController._transport,
                    _profilePlayer.Abilities,
                    _profilePlayer.Abilities,
                    _placeForUi);
                break;
            case GameState.Settings:
                DisposeControllers();
                _setingsController = new SettingsController(_placeForUi,_profilePlayer);
                break;
            case GameState.Shed:
                DisposeControllers();
                _shedController = new ShedController(_placeForUi, _profilePlayer, _upgradeItemConfigs);
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
        _setingsController?.Dispose();
        _shedController?.Dispose();
        _abilitiesController?.Dispose();
    }
}
