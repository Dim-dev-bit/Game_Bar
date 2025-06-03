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

        public Text timerText;

        // Use this for initialization
        void Start()
        {
            currentTime = restartTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (isTimerRunning)
            {
                currentTime -= Time.deltaTime;

                if (timerText != null)
                {
                    timerText.text = $"Time: {Mathf.Max(0, Mathf.Ceil(currentTime))}";
                }

                if (currentTime <= 0)
                {
                    RestartGame();
                }
            }
        }

        void RestartGame()
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            

        }
    }
}