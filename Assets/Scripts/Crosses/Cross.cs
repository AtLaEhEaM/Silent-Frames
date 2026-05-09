using TMPro;
using UnityEngine;

public class Cross : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI text;

    public void Interact()
    {
        Inv.instance.crosses++;
        text.text = "X" + Inv.instance.crosses;
        this.gameObject.SetActive(false);
    }
}
