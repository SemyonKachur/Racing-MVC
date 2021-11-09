using UnityEngine;

internal class Enemy : IEnemy
{
    private string _name;

    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;

    public Enemy(string name)
    {
        _name = name;
    }

    public void Update(DataPlayer dataPlayer)
    {
      _healthPlayer = dataPlayer.CountHealth;
      _moneyPlayer = dataPlayer.CountMoney;
      _powerPlayer = dataPlayer.CountPower;
    }
    public int Power
    {
        get
        {
            var power = _moneyPlayer + _healthPlayer - _powerPlayer;
            return power;
        }
    }

    public int Crime
    {
        get
        {
            var crime = 5 -_powerPlayer + _moneyPlayer;
            return crime;
        }
    }
}
