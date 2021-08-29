using Ui;
using Game;
using Profile;
using UnityEngine;

internal class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private SettingsController _setingsController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _setingsController?.Dispose();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose();
                _setingsController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer);
                _mainMenuController?.Dispose();
                _setingsController?.Dispose();
                break;
            case GameState.Settings:
                _setingsController = new SettingsController(_placeForUi,_profilePlayer);
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _setingsController?.Dispose();
                break;
        }
    }
}
