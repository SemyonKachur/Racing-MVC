using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(15f, GameState.Start);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    protected void OnDestroy()
    {
        _mainController.Dispose();
    }
}
