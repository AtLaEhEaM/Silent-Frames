using TMPro;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    public GameObject[] crosses;
    int cross = 0;
    public int c = 1;
    public LocalTeleport teleport;
    public GameObject statue;
    public TextMeshProUGUI text;

    public void Interact()
    {
        if(Inv.instance.crosses <= 0)
        {
            return;
        }
        crosses[cross].SetActive(true);
        
        Inv.instance.crosses--;
        cross++;
        text.text = "X" + Inv.instance.crosses;
        c++;
        if ((c == 4))
        {
          teleport.Teleport();  
        }
    }


}
