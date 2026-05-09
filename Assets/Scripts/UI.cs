using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public int sceneIndex;
    public GameObject credits;
    public void Play()
    {
                SceneManager.LoadScene(sceneIndex);
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
