using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Madness.SceneManagement
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        static bool hasSpawned;

        private void Start()
        {
            if (hasSpawned) return;
            DontDestroyOnLoad(this.gameObject);
            hasSpawned = true;
        }
    }
}