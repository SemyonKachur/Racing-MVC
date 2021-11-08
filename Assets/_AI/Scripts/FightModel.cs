namespace AI
{
    internal class FightModel
    {
        public Enemy Enemy { get; private set; }

        public Money Money { get; private set; }
        public Health Health { get; private set; } 
        public Power Power { get; private set; }
        
        public int AllCountMoneyPlayer { get; private set; }
        public int AllCountHealthPlayer { get; private set; } 
        public int AllCountPowerPlayer { get; private set; }

        public FightModel()
        {
            Enemy = new Enemy("Flappy");

            Money = new Money(nameof(Money));
            Money.Attach(Enemy);

            Health = new Health(nameof(Health));
            Health.Attach(Enemy);

            Power = new Power(nameof(Power));
            Power.Attach(Enemy);
        }
        
        public void ChangePower(bool isAddCount)
        {
            if (isAddCount)
                AllCountPowerPlayer++;
            else
                AllCountPowerPlayer--;
        }

        public void ChangeHealth(bool isAddCount)
        {
            if (isAddCount)
                AllCountHealthPlayer++;
            else
                AllCountHealthPlayer--;
        }

        public void ChangeMoney(bool isAddCount)
        {
            if (isAddCount)
                AllCountMoneyPlayer++;
            else
                AllCountMoneyPlayer--;
        }
    }
    
}