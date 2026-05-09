using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class JumpScare : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject killCam;
    public Transform lookAtTarget;

    [Header("Movement")]
    public Vector3 killCamTargetLocalPos;
    public float moveDuration = 1f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip jumpscareSFX;

    [Header("Scene Reload")]
    public float reloadDelay = 2f;
    public GameObject deathScreen;

    bool triggered;
    NavMeshAgent ai;

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerJumpScare();
        }
    }

    public void TriggerJumpScare()
    {
        if (triggered)
            return;

        killCam.SetActive(true);
        triggered = true;

        StartCoroutine(JumpScareRoutine());
    }

    IEnumerator JumpScareRoutine()
    {
        if (ai != null)
        {
            ai.isStopped = true;
            ai.enabled = false;
        }

        player.SetActive(false);

        if (jumpscareSFX != null)
            audioSource.PlayOneShot(jumpscareSFX);

        Vector3 startPos = killCam.transform.localPosition;
        Vector3 targetPos = killCamTargetLocalPos;

        float t = 0f;

        while (t < moveDuration)
        {
            t += Time.deltaTime;

            killCam.transform.localPosition =
                Vector3.Lerp(startPos, targetPos, t / moveDuration);

            if (lookAtTarget != null)
            {
                killCam.transform.rotation =
                    Quaternion.LookRotation(
                        lookAtTarget.position - killCam.transform.position
                    );
            }

            yield return null;
        }

        killCam.transform.localPosition = targetPos;

        if (lookAtTarget != null)
        {
            killCam.transform.rotation =
                Quaternion.LookRotation(
                    lookAtTarget.position - killCam.transform.position
                );
        }

        deathScreen.SetActive(true);

        yield return new WaitForSeconds(reloadDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}