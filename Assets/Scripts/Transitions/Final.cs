using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalTeleport : MonoBehaviour
{
    public GameObject player;
    public GameObject cc;
    public Vector3 targetLocalPosition;
    public eheheh e;

    public void Teleport()
    {
        StartCoroutine(endingroutine());
    }

    IEnumerator endingroutine()
    {
        e.pppp();
        player.SetActive(false);
        cc.SetActive(true);
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene(0);
    }
}