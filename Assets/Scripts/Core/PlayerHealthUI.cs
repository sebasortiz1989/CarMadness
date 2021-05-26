using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Car.Core;

namespace Car.UI
{ 
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] Health health;
        [SerializeField] RectTransform foreground;

        void OnEnable()
        {
            health.onHealthUpdated += OnUpdate;
        }

        void OnDisable()
        {
            health.onHealthUpdated -= OnUpdate;
        }

        void Start()
        {
            foreground.localScale = new Vector3(1, 1, 1);    
        }

        void OnUpdate()
        {
            foreground.localScale = new Vector3(health.GetHealthFraction(), 1, 1);   
        }
    }
}