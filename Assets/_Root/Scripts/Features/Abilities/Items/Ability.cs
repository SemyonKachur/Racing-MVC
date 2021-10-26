namespace Features.Abilities.Items
{
    internal class Ability : IAbility
    {
        public int Id { get; }
        public AbilityInfo Info { get; private set; }
        
        public Ability(int id, AbilityInfo info)
        {
            Id = id;
            Info = info;
        }
        
        public void Apply(IAbilityActivator activator)
        {
            throw new System.NotImplementedException();
        }
 
    }
}