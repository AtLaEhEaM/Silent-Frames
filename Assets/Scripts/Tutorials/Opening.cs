using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Opening : MonoBehaviour
{
    public Image img;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        img.gameObject.SetActive(true);

        Color c = img.color;
        c.a = 1f;
        img.color = c;

        float t = 0f;
        float duration = 2f;

        while (t < duration)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(1f, 0f, t / duration);
            img.color = c;

            yield return null;
        }

        c.a = 0f;
        img.color = c;

        img.gameObject.SetActive(false);
    }
}
