using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameTimer : MonoBehaviour
    {
        public float restartTime; 
        private float currentTime;
        private bool isTimerRunning = true;
        public GameObject gameOverPanel;
        public GameObject hudCanvas;
        public GameObject hudCanvas1;

        public Text timerText;

        // Use this for initialization
        void Start()
        {
            currentTime = restartTime;
        }

        // Update is called once per frame
        void Update()
        {
            hudCanvas1.SetActive(true);
            if (isTimerRunning)
            {
                currentTime -= Time.deltaTime;

                if (timerText != null)
                {
                    timerText.text = $"Time: {Mathf.Max(0, Mathf.Ceil(currentTime))}";
                }

                if (currentTime <= 0)
                {
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            Time.timeScale = 0f;
            isTimerRunning = false;
            // Показываем панель
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
                hudCanvas.SetActive(false);
                hudCanvas1.SetActive(false);
            }

        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            

        }
    }
}