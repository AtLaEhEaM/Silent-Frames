using UnityEngine;

public class Cross : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Inv.instance.crosses++;
        this.gameObject.SetActive(false);
    }
}
