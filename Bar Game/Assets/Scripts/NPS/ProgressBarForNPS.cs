using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class ProgressBarForNPS : MonoBehaviour
{
    public Image progressBar; // Ссылка на Image компонента прогресс-бара
    public GameObject BackProgressBar;
    public float elapsedTime { get; private set; }

    protected void Awake()
    {
        progressBar.fillAmount = 0;
        progressBar.gameObject.SetActive(false);
        BackProgressBar.SetActive(false);
    }

    public void StartProgress(float givenTime)
    {
        Debug.Log("ше цщклч");
        progressBar.gameObject.SetActive(true);
        BackProgressBar.SetActive(true);
        StartCoroutine(ProgressCoroutine(givenTime));
    }

    private IEnumerator ProgressCoroutine(float givenTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime - givenTime < 0f)
        {
            elapsedTime += Time.deltaTime;
            progressBar.fillAmount = elapsedTime / givenTime;
            Debug.Log(elapsedTime);
            yield return null;

        }
        progressBar.fillAmount = 0f;
        elapsedTime = 0f; // Сброс переменной elapsedTime
        Debug.Log("Was here!");

        progressBar.gameObject.SetActive(false);
        BackProgressBar.SetActive(false);

        yield return null;
    }

    public void Renewed(float givenTime)
    {
        Awake();

        progressBar.gameObject.SetActive(true);
        BackProgressBar.SetActive(true);
        StartCoroutine(ProgressCoroutine(givenTime));
    }
}

