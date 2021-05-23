using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        // Config
        [SerializeField] float fadeInTime = 1f;

        // Initialize variables
        CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        void Start()
        {
            StartCoroutine(FadeIn(fadeInTime));
        }

        private IEnumerator FadeIn(float time)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 1;
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
            gameObject.SetActive(false);
        }


    }
}
