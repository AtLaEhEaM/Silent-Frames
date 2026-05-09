using UnityEngine;
using UnityEngine.SceneManagement;

public class OneToTwo : MonoBehaviour
{
    public string sceneName = "Scene2";

    public void OnTriggerEnter(Collider other)
    {
        LoadingScreen.instance.StartLoading();
        SceneManager.LoadScene(sceneName);
    }
}
