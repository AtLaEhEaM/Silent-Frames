using TMPro;
using UnityEngine;

public class ActivateFlashLight : MonoBehaviour
{
    public TextMeshProUGUI flashLighttext;

    public void DisplayText()
    {
                flashLighttext.text = "Press /'F/' to toggle Flashlight";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Destroy(this);
        }
    }
}
