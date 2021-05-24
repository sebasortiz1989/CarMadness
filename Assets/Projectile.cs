using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Car.Combat
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody rb;
        Weapon weapon;
        [SerializeField] float speed = 100f;
        bool shouldExplode = false;

        void Awake()
        {
            rb = GetComponent<Rigidbody>(); 
        }       

        void FixedUpdate()
        {
            if (shouldExplode)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, weapon.GetExplosionRadius());
                foreach (Collider hit in hits)
                {
                    if (hit.gameObject.tag == "Car")
                    {
                        AIController aiController = hit.GetComponent<AIController>();
                        if (aiController == null) return;

                        aiController.FreezeMovementFromExplosion();

                        Rigidbody hitRB = aiController.GetRigidBody();
                        hitRB.AddExplosionForce(weapon.GetExplosionForce(), transform.position, weapon.GetExplosionRadius(), 1, ForceMode.Impulse);
                    }
                }
                shouldExplode = false;
            }
        } 

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.tag == "Car")
            {
                print("hit " + gameObject.name);
                // should do damage and a small amount of knockback force
                    // if killed then they can be blown off screen
                    // once they make contact with anything they can explode for good
                shouldExplode = true;
            }   
            else if (other.gameObject.tag == "Player")
            {
                // TODO
            } 
        }

        public void LaunchProjectile(Transform launchTransform, Weapon weapon)
        {
            this.weapon = weapon;
            rb.AddForce(launchTransform.forward * speed * Time.deltaTime);
        }

    }
}
