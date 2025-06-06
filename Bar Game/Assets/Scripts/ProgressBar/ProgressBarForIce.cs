using BarGame.ProgressBar;
using System.Collections;
using UnityEngine;

namespace BarGame.ProgressBar {
    public class ProgressBarForIce : ProgressBarBase {

        [SerializeField] private int maxIceAmount = 5;
        [SerializeField] private float IceGeneratorInterval = 3f;
        [SerializeField] private Transform IceGeneratorTransform;


        public int currentAmount = 0;

        public void StartProgress()
        {
            gameObject.SetActive(true);  // Активируем основной объект ПЕРВЫМ!
            targetTime = maxIceAmount;
            currentFillAmount = 0f;
            progressBar.gameObject.SetActive(true);
            backProgressBar.SetActive(true);
            IsActive = true;
            progressCoroutine = StartCoroutine(ProgressCoroutine());
        }
        protected override IEnumerator ProgressCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(IceGeneratorInterval);
                if (currentAmount < maxIceAmount)
                {
                    currentAmount++;
                    Debug.Log(currentAmount);
                    UpdateProgress((float)currentAmount);
                    Debug.Log($"Ice generated: {(float)currentAmount / maxIceAmount}");
                }
            }
        }

        protected void Update()
        {
            transform.parent.position = IceGeneratorTransform.position;
        }
    }
}