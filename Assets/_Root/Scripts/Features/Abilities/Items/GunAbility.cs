using System;
using Inventory;
using JetBrains.Annotations;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Abilities.Items
{
    internal class GunAbility : IAbility
    {
        private readonly Rigidbody2D _viewPrefab;
        private readonly ResourcePath viewPath = new ResourcePath("Prefabs/Abilities/Bomb");
        private readonly float _projectileSpeed;
        
        public int Id { get; }
        public ItemInfo Info { get; }

        public GunAbility(
            string viewPath,
            float projectileSpeed)
        {
            _viewPrefab = ResourcesLoader.LoadObject(this.viewPath);    
            if (_viewPrefab == null) throw new InvalidOperationException($"{nameof(GunAbility)} view requires {nameof(Rigidbody2D)} component!");
            _projectileSpeed = projectileSpeed;
        }
  
        public void Apply(IAbilityActivator activator)
        {
            var projectile = Object.Instantiate(_viewPrefab);
            projectile.AddForce(activator.GetViewObject().transform.right * _projectileSpeed, ForceMode2D.Force);
        }
    }

}