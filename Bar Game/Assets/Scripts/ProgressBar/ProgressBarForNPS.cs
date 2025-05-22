using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace BarGame.ProgressBar {
    public class ProgressBarForNPS : ProgressBarBase {
        protected override IEnumerator ProgressCoroutine()
        {
            float elapsedTime = 0f;
            while (elapsedTime < targetTime)
            {
                elapsedTime += Time.deltaTime;
                UpdateProgress(elapsedTime);
                yield return null;

            }

            CompleteProgress();
        }

        private void CompleteProgress()
        {
            Debug.Log("NPS progress finished!");
            ResetProgress();
        }
        public void Renewed(float givenTime)
        {
            ResetProgress();
            StartProgress(givenTime);
        }
    }
}

