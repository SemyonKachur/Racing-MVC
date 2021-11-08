using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

internal class FightWindowView : MonoBehaviour
{
    [Header("Player configs")]
    [field: SerializeField] public TMP_Text CountMoneyText;
    [field: SerializeField] public TMP_Text CountHealthText;
    [field: SerializeField] public TMP_Text CountPowerText;

    [Header("Enemy configs")]
    [field: SerializeField] public TMP_Text CountPowerEnemyText;
    [field: SerializeField] public TMP_Text CountCrimeEnemyText;

    [Header("Money")]
    [field: SerializeField] public Button AddMoneyButton;
    [field: SerializeField] public Button MinusMoneyButton;

    [Header("Health")]
    [field: SerializeField] public Button AddHealthButton;
    [field: SerializeField] public Button MinusHealthButton;

    [Header("Power")]
    [field: SerializeField] public Button AddPowerButton;
    [field: SerializeField] public Button MinusPowerButton;
    
    [Header("Action buttons")]
    [field: SerializeField] public Button FightButton;
    [field: SerializeField] public Button SkipFight;

    [Header("Fighters")] 
    [field: SerializeField] public Image Player;
    [field: SerializeField] public Image Enemy;


    public void Init(
        UnityAction<bool> changeMoney,
        UnityAction<bool> changeHealth, 
        UnityAction<bool> changePower, 
        UnityAction fight, 
        UnityAction skipFight)
    {

        AddMoneyButton.onClick.AddListener(() => changeMoney(true));
        MinusMoneyButton.onClick.AddListener(() => changeMoney(false));

        AddHealthButton.onClick.AddListener(() => changeHealth(true));
        MinusHealthButton.onClick.AddListener(() => changeHealth(false));

        AddPowerButton.onClick.AddListener(() => changePower(true));
        MinusPowerButton.onClick.AddListener(() => changePower(false));

        FightButton.onClick.AddListener(fight);
        SkipFight.onClick.AddListener(skipFight);
    }
}
