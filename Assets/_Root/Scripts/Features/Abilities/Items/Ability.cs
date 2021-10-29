using Inventory;

namespace Features.Abilities.Items
{
    internal class Ability : IAbility
    {
        public int Id { get; }
        public ItemInfo Info { get; private set; }
        
        public Ability(int id, ItemInfo info)
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