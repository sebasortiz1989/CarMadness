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

        Transform leftLaunchTransform;
        Transform rightLaunchTransform;

        void Start()
        {
            Setup();
        }

        private void Setup()
        {
            GameObject leftWeapon = Instantiate(weapon.GetPrefab(), leftWeaponTransform);
            leftLaunchTransform = leftWeapon.GetComponent<Cannon>().GetLaunchTransform();
            //leftLaunchTransform.position += new Vector3(1.041f, 0, 2.33f); 


            GameObject rightWeapon = Instantiate(weapon.GetPrefab(), rightWeaponTransform);
            //rightLaunchTransform.position += new Vector3(1.041f, 0, 2.33f); 
            rightLaunchTransform = rightWeapon.GetComponent<Cannon>().GetLaunchTransform();
        }

        public void FireWeapon() // currently fires two projectiles, one for each side
        {
            // TODO Get projectiles from object pool
            Projectile leftProj = Instantiate(weapon.GetProjectile(), leftLaunchTransform.position, Quaternion.identity);
            Projectile rightProj = Instantiate(weapon.GetProjectile(), rightLaunchTransform.position, Quaternion.identity);

            leftProj.LaunchProjectile(leftLaunchTransform, weapon);
            rightProj.LaunchProjectile(rightLaunchTransform, weapon);

        }
    }

}