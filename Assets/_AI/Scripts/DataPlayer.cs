using System.Collections.Generic;
using UnityEngine;

internal class DataPlayer
{
    private const string WoodKey = nameof(WoodKey);
    private const string DiamondKey = nameof(DiamondKey);
    private const string HealthKey = nameof(HealthKey);
    private const string PowerKey = nameof(PowerKey);
    private const string MoneyKey = nameof(MoneyKey);
    
    private string _titleData;

    private int _countMoney;
    private int _countHealth;
    private int _countPower;

    private List<IEnemy> _enemies = new List<IEnemy>();

    public DataPlayer()
    {
        
    }

    public string TitleData => _titleData;

    public int CountMoney 
    { 
        get => PlayerPrefs.GetInt(MoneyKey);
        set
        {
            if (PlayerPrefs.GetInt(MoneyKey) != value)
            {
                PlayerPrefs.SetInt(MoneyKey,value);
                Notifier(DataType.Money);
            }
        }
    }

    public int CountHealth
    {
        get => PlayerPrefs.GetInt(HealthKey);
        set
        {
            if (PlayerPrefs.GetInt(HealthKey) != value)
            {
                PlayerPrefs.SetInt(HealthKey, value);
                Notifier(DataType.Health);
            }
        }
    }

    public int CountPower
    {
        get => PlayerPrefs.GetInt(PowerKey);
        set
        {
            if (PlayerPrefs.GetInt(PowerKey) != value)
            {
                PlayerPrefs.SetInt(PowerKey, value);
                Notifier(DataType.Power);
            }
        }
    }

    public void Attach(IEnemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void Detach(IEnemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private void Notifier(DataType dataType)
    {
        foreach(var enemy in _enemies)
            enemy.Update(this);
    }
}

internal class Money : DataPlayer
{
    public Money(string titleData)
    {
        
    }
}

internal class Health : DataPlayer
{
    public Health(string titleData)
    {
    }
}

internal class Power : DataPlayer
{
    public Power(string titleData)
    {
    }
}
