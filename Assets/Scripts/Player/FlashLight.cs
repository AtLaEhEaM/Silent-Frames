using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip FlashLightCLick;
    public bool isOn = false;
    public Light light;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            audioSource.PlayOneShot(FlashLightCLick);
            if(isOn)             {
                light.enabled = false;
                isOn = false;
            }
            else
            {
                light.enabled = true;
                isOn = true;
            }
        }
    }
}
