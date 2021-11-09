namespace AI
{
    internal class FightModel
    {
        public Enemy Enemy { get; private set; }
        public DataPlayer DataPlayer { get; private set; }
        public Money Money { get; private set; }
        public Health Health { get; private set; } 
        public Power Power { get; private set; }

        public FightModel()
        {
            Enemy = new Enemy("Flappy");
            
            Money = new Money(nameof(Money));
            Money.Attach(Enemy);

            Health = new Health(nameof(Health));
            Health.Attach(Enemy);

            Power = new Power(nameof(Power));
            Power.Attach(Enemy);

            DataPlayer = new DataPlayer();
        }
        
        public void ChangePower(bool isAddCount)
        {
            if (isAddCount)
                Power.CountPower++;
            else
                Power.CountPower--;
        }

        public void ChangeHealth(bool isAddCount)
        {
            if (isAddCount)
                Health.CountHealth++;
            else
                Health.CountHealth--;
        }

        public void ChangeMoney(bool isAddCount)
        {
            if (isAddCount)
                Money.CountMoney++; 
            else
                Money.CountMoney--;
                
        }
    }
    
}