using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Car.Core;

namespace Car.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] RectTransform foreground;
        [SerializeField] Canvas rootCanvas;
        [SerializeField] Health health;

        void OnEnable()
        {
            health.onHealthUpdated += SetHealth;
        }

        void OnDisable() 
        {
            health.onHealthUpdated -= SetHealth;
        }

        // void Start()
        // {
        //     rootCanvas.gameObject.SetActive(false);

        // }

        private void SetHealth()
        {
            MakeHealthBarVisible();
            foreground.localScale = new Vector3 (health.GetHealthFraction(), 1, 1);
            if (Mathf.Approximately(health.GetHealthFraction(), 0))
            {
                rootCanvas.gameObject.SetActive(false);
            }
        }

        private void MakeHealthBarVisible()
        {
            if (Mathf.Approximately(health.GetHealthFraction(), 1) || 
                        Mathf.Approximately(health.GetHealthFraction(), 0))
            {
                rootCanvas.gameObject.SetActive(false);
            }
            else
            {
                rootCanvas.gameObject.SetActive(true);
            }
        }
    }
}
