using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth;
        public float health;

        public event Action onHealthUpdated;

        void Start()
        {
            health = maxHealth;
        }

        public void AffectHealth(float delta)
        {
            health += delta;
            health = Mathf.Clamp(health, 0, maxHealth);

            onHealthUpdated();

            if (health <= 0)
            {
                Die();
            }
            
        }

        public float GetHealthFraction()
        {
            if (maxHealth <= 0)
            {
                Debug.Log("Max Health of " + transform.gameObject.name + " is 0");
                maxHealth = 1;
            }

            return health / maxHealth;
        }

        private void Die()
        {
            print(gameObject.name + " has died.");
            GetComponent<AIController>().Die();
        }


    }

}