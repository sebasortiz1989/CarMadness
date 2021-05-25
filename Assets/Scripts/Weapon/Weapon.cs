using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Combat
{    
    [CreateAssetMenu(fileName = "Weapon", menuName = "CarMadness/Weapon", order = 0)]
    public class Weapon : ScriptableObject 
    {
        
        [SerializeField] GameObject prefab;
        [Tooltip("If false, will raycast rather than lob a projectile")]
        [SerializeField] bool isProjectile = false;
        [SerializeField] Projectile projectilePrefab;
        [SerializeField] float projectileSpeed = 100f;
        [SerializeField] float explosionForce = 1500f;
        [SerializeField] float explosionRadius = 5f;
        [Tooltip("A small amount of force applied when the projectile hits an enemy. Should be a small amount as they will explode off scene with explosion force when they die")]
        [SerializeField] float initialHitForce = 300f;
        [SerializeField] float damage = 10f;
        //[SerializeField] 
        // particle systems

        public GameObject GetPrefab()
        {
            return prefab;
        }

        public Projectile GetProjectile()
        {
            return projectilePrefab;
        }

        public float GetProjectileSpeed()
        {
            return projectileSpeed;
        }

        public float GetExplosionForce()
        {
            return explosionForce;
        }

        public float GetInitialHitForce()
        {
            return initialHitForce;
        }

        public float GetExplosionRadius()
        {
            return explosionRadius;
        }

        public float GetDamage()
        {
            return damage;
        }
    }
    

}