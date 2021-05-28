using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Car.Control;

namespace Car.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth;
        [SerializeField] GameObject deathFX;
        [SerializeField] Transform fxParent;
        [SerializeField] GameObject[] objsToOffOnDeath;
        public float health;

        public event Action onHealthUpdated;

        void Start()
        {
            health = maxHealth;
            onHealthUpdated();
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

            AIController aiController = GetComponent<AIController>();
            if (aiController != null)
            {
                aiController.Die();
                GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
                fx.transform.parent = fxParent;
                Destroy(this.gameObject, 0.2f);
            }
            else // is player
            {
                GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
                fx.transform.parent = fxParent;

                foreach(GameObject o in objsToOffOnDeath)
                {
                    o.SetActive(false);
                }
            }
        }
    }
}
