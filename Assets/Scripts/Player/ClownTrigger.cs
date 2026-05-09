using System.Collections;
using UnityEngine;

public class ClownTrigger : MonoBehaviour
{
    public GameObject clown;

    bool moving;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !moving)
        {
            StartCoroutine(TriggerSequence());
        }
    }

    IEnumerator TriggerSequence()
    {
        moving = true;

        yield return StartCoroutine(MoveDown());

        yield return new WaitForSeconds(5f);

        moving = false;
    }

    IEnumerator MoveDown()
    {
        Vector3 startPos = clown.transform.position;
        Vector3 targetPos = startPos + Vector3.down * 3f;

        float duration = 2f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            clown.transform.position =
                Vector3.Lerp(startPos, targetPos, t / duration);

            yield return null;
        }

        clown.transform.position = targetPos;
    }
}