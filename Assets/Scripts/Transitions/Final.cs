using UnityEngine;

public class LocalTeleport : MonoBehaviour
{
    public GameObject player;
    public GameObject cc;
    public Vector3 targetLocalPosition;

    public void Teleport()
    {
        player.SetActive(false);
        cc.SetActive(true);
    }
}