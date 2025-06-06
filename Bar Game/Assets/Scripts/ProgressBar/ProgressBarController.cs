using System.Collections;
using UnityEngine;

namespace BarGame.ProgressBar {
    public class ProgressBarController : ProgressBarBase {
        protected override IEnumerator ProgressCoroutine()
        {
            float elapsedTime = 0f;
            // Если клавиша отпущена - прерываем прогресс
            while (elapsedTime < targetTime)
            {
                // Если клавиша отпущена - прерываем прогресс
                if (!Input.GetKey(KeyCode.DownArrow))
                {
                    ResetProgress();
                    yield break;
                }

                elapsedTime += Time.deltaTime;
                UpdateProgress(elapsedTime);
                yield return null; // Важно: приостанавливаем корутину на кадр
            }

            CompleteProgress();
        }

        private void CompleteProgress()
        {
            Debug.Log("Progress complete!");
            ResetProgress();
        }
    }
}
