using Profile;
using Tool;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;

    private readonly ResourcePath _resourcePath = new ResourcePath("ScriptableObjects/ProfilePlayerCar");
    private ProfilePlayerSO _profilePlayerSO;
    private MainController _mainController;

    private void Awake()
    {
        _profilePlayerSO = ResourcesLoader.LoadProfilePlayer(_resourcePath);
        var profilePlayer = new ProfilePlayer(_profilePlayerSO);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    protected void OnDestroy()
    {
        _mainController.Dispose();
    }
}
