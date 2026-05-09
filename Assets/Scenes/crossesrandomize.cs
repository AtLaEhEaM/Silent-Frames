using UnityEngine;
using System.Collections.Generic;

public class RandomActivator : MonoBehaviour
{
    public GameObject[] objects;
    public int keepActiveCount = 3;

    void Start()
    {
        ApplyRandomSelection();
    }

    public void ApplyRandomSelection()
    {
        if (objects == null || objects.Length == 0)
            return;

        List<GameObject> pool = new List<GameObject>(objects);

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }

        for (int i = 0; i < keepActiveCount && pool.Count > 0; i++)
        {
            int index = Random.Range(0, pool.Count);

            GameObject chosen = pool[index];
            pool.RemoveAt(index);

            chosen.SetActive(true);
        }
    }
}