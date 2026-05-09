using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public int sceneIndex;
    public GameObject credits;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
                SceneManager.LoadScene(sceneIndex);
        LoadingScreen.instance.StartLoading();
    }

    public void Quit()
        {
            Application.Quit();
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void Back()
    {
               credits.SetActive(false);

    }
}
