using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Car.Combat;

namespace Car.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] Transform leftWeaponTransform;
        [SerializeField] Transform rightWeaponTransform;
        [SerializeField] float attackAngle = 30f;

        bool cooldown = false;

        Transform leftLaunchTransform;
        Transform rightLaunchTransform;

        void Start()
        {
            Setup();
            cooldown = true;
            StartCoroutine(Cooldown());
        }

        private void Setup()
        {
            if (leftWeaponTransform != null)
            {
                GameObject leftWeapon = Instantiate(weapon.GetPrefab(), leftWeaponTransform);
                leftLaunchTransform = leftWeapon.GetComponent<Cannon>().GetLaunchTransform();
            }
            
            // if (rightWeaponTransform != null)
            // {
            //     GameObject rightWeapon = Instantiate(weapon.GetPrefab(), rightWeaponTransform);
            //     rightLaunchTransform = rightWeapon.GetComponent<Cannon>().GetLaunchTransform();
            // }
            
        }

        public void Attack(Transform target)
        {
            Vector3 direction = new Vector3(target.position.x - transform.position.x,
                                            transform.position.y,
                                            target.position.z - transform.position.z);
            if (Vector3.Angle(transform.forward, direction) < attackAngle
                            && !cooldown)
            {
                FireWeapon();

                cooldown = true;
                StartCoroutine(Cooldown());
            }
        }

        IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(weapon.GetAttackCooldownSeconds());
            cooldown = false;
        }

        public void FireWeapon() // currently fires two projectiles, one for each side
        {
            // TODO Get projectiles from object pool
            Projectile leftProj = Instantiate(weapon.GetProjectile(), leftLaunchTransform.position, Quaternion.identity);
            //Projectile rightProj = Instantiate(weapon.GetProjectile(), rightLaunchTransform.position, Quaternion.identity);

            leftProj.SetupProjectile(leftLaunchTransform, weapon, this.gameObject);
            //rightProj.SetupProjectile(rightLaunchTransform, weapon);

        }
    }

}