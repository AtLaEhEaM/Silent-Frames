using System.Collections;
using UnityEngine;

public class LVLAmbient : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public Vector2 delay = new Vector2(2f, 8f);

    private void Start()
    {
        StartCoroutine(PlayAmbient());
    }

    IEnumerator PlayAmbient()
    {
        while (true)
        {
            float randomDelay = Random.Range(delay.x, delay.y);

            yield return new WaitForSeconds(randomDelay);

            audioSource.PlayOneShot(clip);
        }
    }
}