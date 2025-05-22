using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BarGame.ProgressBar {
    public abstract class ProgressBarBase : MonoBehaviour {
        [SerializeField] protected Image progressBar;
        [SerializeField] protected GameObject backProgressBar;

        protected float currentFillAmount;
        protected float targetTime;

        public Coroutine progressCoroutine;

        public bool IsActive { get; protected set; }

        protected virtual void Awake()
        {
            ResetProgress();
        }

        public virtual void ResetProgress()
        {
            if (progressCoroutine != null)
                StopCoroutine(progressCoroutine);
            currentFillAmount = 0;
            progressBar.fillAmount = 0f;
            progressBar.gameObject.SetActive(false);
            backProgressBar.SetActive(false);
            IsActive = false;
        }

        public virtual void StartProgress(float duration)
        {
            gameObject.SetActive(true);  // Активируем основной объект ПЕРВЫМ!

            targetTime = duration;
            currentFillAmount = 0f;
            progressBar.gameObject.SetActive(true);
            backProgressBar.SetActive(true);
            IsActive = true;
            progressCoroutine = StartCoroutine(ProgressCoroutine());
        }

        protected abstract IEnumerator ProgressCoroutine();

        public virtual void UpdateProgress(float elapsed)
        {
            currentFillAmount = elapsed / targetTime;
            progressBar.fillAmount = currentFillAmount;
        }
    }
}