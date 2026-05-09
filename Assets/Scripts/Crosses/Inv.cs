using UnityEngine;

public class Inv : MonoBehaviour
{
    public static Inv instance;
    public int crosses = 0;

    private void Awake()
    {
        instance = this;
    }
}
