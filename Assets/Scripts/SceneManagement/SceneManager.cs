using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Madness.SceneManagement
{
    public class SceneManager : MonoBehaviour
    {
        // Initialize Variables
        int currentSceneIndex;

        // Start is called before the first frame update
        void Start()
        {
            currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        }

        public void LoadGarage()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        public void LoadMainScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }

        public void LoadNextScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1);
        }

        public void LoadPreviousScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex - 1);
        }

        public void RestartLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
        }

        public void LoadGameOver()
        {
            StartCoroutine(GameOver());
        }

        IEnumerator GameOver()
        {
            yield return new WaitForSeconds(3f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScreen");
        }


        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
        }
    }
}