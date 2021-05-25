using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Car.Combat
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody rb;
        Weapon weapon;
        [SerializeField] ParticleEmissionStopper emissionStopper;
        [SerializeField] bool destroyOnContact = true;
        bool shouldExplode = false;
        bool isLaunching = false;
        Transform launchTransform;

        void Awake()
        {
            rb = GetComponent<Rigidbody>(); 
        }   

        void LateUpdate()
        {
            // if (isLaunching)
            // {
            //     transform.Translate(launchTransform.forward * weapon.GetProjectileSpeed() * Time.deltaTime);

            // }    
        }        

        void FixedUpdate()
        {
            if (isLaunching)
            {
                rb.AddForce(launchTransform.forward * weapon.GetProjectileSpeed() * Time.deltaTime);
                isLaunching = false;

            }
            if (shouldExplode)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, weapon.GetExplosionRadius());
                foreach (Collider hit in hits)
                {
                    if (hit.gameObject.tag == "Car")
                    {
                        AIController aiController = hit.GetComponent<AIController>();
                        if (aiController == null) return;

                        aiController.HitByWeapon(weapon);                      
                        
                        if (!aiController.GetIsDead())
                        {
                            Rigidbody hitRB = aiController.GetSphereRigidBody();
                            aiController.FreezeMovementFromHit(0.5f);
                            Vector3 forceDirection = transform.position - hit.transform.position;
                            hitRB.AddForce(forceDirection.normalized * weapon.GetInitialHitForce());
                        }
                        else
                        {
                            Rigidbody hitRB = aiController.GetBodyRigidBody();
                            aiController.FreezeMovementFromExplosion(3f);
                            hitRB.AddExplosionForce(weapon.GetExplosionForce(), transform.position, weapon.GetExplosionRadius(), 1, ForceMode.Impulse);
                            
                        }                                              
                                                         
                    }
                }
                shouldExplode = false;
            }
        } 

        private void OnCollisionEnter(Collision other) // TODO bouncy projectiles?
        {          
            if (other.gameObject.tag == "Car")
            {              
                StopEmissionsAndDestroy(3f);
                // should do damage and a small amount of knockback force
                    // if killed then they can be blown off screen
                    // once they make contact with anything they can explode for good
                shouldExplode = true;
            }   
            else if (other.gameObject.tag == "Player")
            {
                // TODO
            } 
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground Layer"))
            {
                print("hit ground");
                StopEmissionsAndDestroy(2f);
            }       
        }

        private void StopEmissionsAndDestroy(float destroyDelay)
        {
            emissionStopper.StopAllParticleEmission();               
                
            if (destroyOnContact)
            {
                Destroy(this.gameObject, destroyDelay); // TODO remove via object pool
            }
        }

        public void LaunchProjectile(Transform launchTransform, Weapon weapon)
        {
            this.weapon = weapon;
            this.launchTransform = launchTransform;
            isLaunching = true;
        }

    }
}
