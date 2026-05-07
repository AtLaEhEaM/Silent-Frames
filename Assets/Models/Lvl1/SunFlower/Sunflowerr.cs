using UnityEngine;

public class Sunflowerr : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sunflower"))
        {
            other.GetComponent<FlowerLookTarget>().lookAtPlayer = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sunflower"))
        {
            other.GetComponent<FlowerLookTarget>().lookAtPlayer = false;
        }
    }
}
