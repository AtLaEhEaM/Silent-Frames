using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public Image loadingImage;
    public TextMeshProUGUI loadingText;

    public static LoadingScreen instance;

    Coroutine loadingRoutine;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void StartLoading()
    {
        loadingImage.gameObject.SetActive(true);

        if (loadingRoutine != null)
            StopCoroutine(loadingRoutine);

        loadingRoutine = StartCoroutine(LoadSceneText());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(CloseLoadingScreen());
    }

    IEnumerator CloseLoadingScreen()
    {
        yield return new WaitForSeconds(1.5f);

        if (loadingRoutine != null)
        {
            StopCoroutine(loadingRoutine);
            loadingRoutine = null;
        }

        loadingImage.gameObject.SetActive(false);
        loadingText.text = "";

    }

    IEnumerator LoadSceneText()
    {
        while (true)
        {
            loadingText.text = "Loading";
            yield return new WaitForSeconds(0.5f);

            loadingText.text = "Loading.";
            yield return new WaitForSeconds(0.5f);

            loadingText.text = "Loading..";
            yield return new WaitForSeconds(0.5f);

            loadingText.text = "Loading...";
            yield return new WaitForSeconds(0.5f);
        }
    }
}