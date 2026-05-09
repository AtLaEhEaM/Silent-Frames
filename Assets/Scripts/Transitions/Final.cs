using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalTeleport : MonoBehaviour
{
    public GameObject player;
    public GameObject cc;
    public Vector3 targetLocalPosition;

    public void Teleport()
    {
        StartCoroutine(endingroutine());
    }

    IEnumerator endingroutine()
    {
        player.SetActive(false);
        cc.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }
}