using System.Collections;
using UnityEngine;
using TMPro;

public class Lvl0A : MonoBehaviour
{
    [Header("References")]
    public AudioSource audioSource;
    public AudioSource playerr;
    public string playyyer;
    public TextMeshProUGUI subtitleText;

    [Header("Dialogue")]
    public AudioClip[] dialogueClips;
    public AudioClip pp;

    [TextArea]
    public string[] subtitles;

    [Header("Settings")]
    public float delayBetweenLines = 1f;

    public bool playOnStart = true;

    bool isPlaying;

    void Start()
    {
        StartCoroutine(starttt());
    }

    IEnumerator starttt()
    {
        yield return new WaitForSeconds(2f);
        playerr.PlayOneShot(pp);
        subtitleText.text = playyyer;
        yield return new WaitForSeconds(pp.length);
        subtitleText.text = "";
        yield return new WaitForSeconds(0.5f);
        PlayDialogue();
    }

    public void PlayDialogue()
    {
        if (isPlaying)
            return;

        StartCoroutine(PlayDialogueRoutine());
    }

    IEnumerator PlayDialogueRoutine()
    {
        isPlaying = true;

        int count = Mathf.Min(dialogueClips.Length, subtitles.Length);

        for (int i = 0; i < count; i++)
        {
            AudioClip clip = dialogueClips[i];

            if (clip == null)
                continue;

            audioSource.clip = clip;
            audioSource.Play();

            subtitleText.text = subtitles[i];

            yield return new WaitForSeconds(clip.length);

            subtitleText.text = "";

            yield return new WaitForSeconds(delayBetweenLines);
        }

        subtitleText.text = "";

        isPlaying = false;
    }
}