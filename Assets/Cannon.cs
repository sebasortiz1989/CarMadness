using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Combat
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] Transform launchTransform;

        public Transform GetLaunchTransform()
        {
            return launchTransform;
        }
    }
}
