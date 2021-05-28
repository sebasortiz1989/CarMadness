using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Car.Core;
using Car.Control;

namespace Car.Combat
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody rb;
        Weapon weapon;
        [SerializeField] ParticleEmissionStopper emissionStopper;
        [SerializeField] float lifeTime = 4f;
        [SerializeField] float fadeTimeBeforeDestroy = 2f;
        bool shouldExplode = false;
        bool shouldStopOnImpact = true;
        bool isLaunching = false;
        bool isExploding = false;
        bool simpleProjectileHit = false;
        Transform launchTransform;
        //Collision target = null;
        Collider target = null;
        GameObject instigator = null;

        void Awake()
        {
            rb = GetComponent<Rigidbody>(); 
        }   

        void Start()
        {
            Invoke("StopParticleEmissions", lifeTime);
            Destroy(this.gameObject, lifeTime + 3f);
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

            
            if (isExploding)
            {
                Explode();
                shouldExplode = false;
            }

            if (simpleProjectileHit)
            {
                SimpleProjectileHit();
                simpleProjectileHit = false;
            }
        } 

        private void Explode()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, weapon.GetExplosionRadius());
                foreach (Collider hit in hits)
                {
                    if (hit.gameObject.tag == "Car")
                    {
                        AIController aiController = hit.GetComponent<AIController>();
                        if (aiController == null) return;

                        aiController.AffectHealth(weapon.GetDamage());                      
                      
                        Rigidbody hitRB = aiController.GetBodyRigidBody();
                        aiController.FreezeMovementFromExplosion(3f);
                        hitRB.AddExplosionForce(weapon.GetExplosionForce(), transform.position, weapon.GetExplosionRadius(), 1, ForceMode.Impulse);                                                         
                    }
                }
        }

        private void SimpleProjectileHit()
        {
            if (target == null) return;
            if (target.gameObject.tag == "Car")
            {
                AIController aiController = target.gameObject.GetComponent<AIController>();
                if (aiController == null) return;

                aiController.AffectHealth(weapon.GetDamage());                      
                      
                Rigidbody hitRB = aiController.GetSphereRigidBody();
                aiController.FreezeMovementFromHit(0.5f);
                Vector3 forceDirection = transform.position - target.transform.position;
                hitRB.AddForce(forceDirection.normalized * weapon.GetHitForce());
                
            }
        }


        

        private void OnTriggerEnter(Collider other) // TODO bouncy projectiles?
        {        
            if (other.gameObject == instigator) return;

            if (other.gameObject.tag == "Car")
            {         
                target = other;     
                if (shouldExplode)
                {           
                    isExploding = true;
                }
                else
                {                   
                    simpleProjectileHit = true;
                }
    
            }   
            else if (other.gameObject.tag == "Player")
            {
                print("hit player");
                Health playerHealth = other.gameObject.GetComponent<Health>();
                playerHealth.AffectHealth(-weapon.GetDamage());
            } 
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground Layer"))
            {
                //print("hit ground");
            
                StopEmissionsAndDestroy(2f);
            }  

            if (shouldStopOnImpact)
            {
                // DisableCollider();
                // StopEmissionsAndDestroy(3f);
            }
            else
            {
                Invoke("StopParticleEmmissions", lifeTime - fadeTimeBeforeDestroy);
                Invoke("DisableCollider", lifeTime - fadeTimeBeforeDestroy);
            
            }   

        }

        private void StopParticleEmissions()
        {
            if (emissionStopper != null)
            {
                emissionStopper.StopAllParticleEmission();

            }
        }

        private void DisableCollider()
        {
            Collider collider = GetComponent<Collider>();
            if (collider)
            {
                collider.enabled = false;
            }
        }

        private void StopEmissionsAndDestroy(float destroyDelay)
        {
            if (emissionStopper != null)
            {
                emissionStopper.StopAllParticleEmission();

            }                                                 
            Destroy(this.gameObject, destroyDelay); // TODO remove via object poo            
        }

        public void SetupProjectile(Transform launchTransform, Weapon weapon, GameObject instigator)
        {
            this.weapon = weapon;
            this.launchTransform = launchTransform;
            this.instigator = instigator;
            shouldExplode = weapon.GetShouldExplode();
            shouldStopOnImpact = weapon.GetShouldProjectileStopOnImpact();
            isLaunching = true;
        }

    }
}
