using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainWindowView : MonoBehaviour
{
    [SerializeField]
    private Button _changeTextButton;

    [SerializeField]
    private Text _changeText;

    private void Start()
    {
        _changeTextButton.onClick.AddListener(ChangeText);
    }

    private void OnDestroy()
    {
        _changeTextButton.onClick.RemoveAllListeners();
    }

    private void ChangeText()
    {
        //_changeText.DOColor("dasdsdadsdadasdsd", 2.0f);
    }
}
