using System.Collections.Generic;
using UnityEngine;

public class RandomChildDeleter : MonoBehaviour
{
    [Range(0f, 1f)]
    public float deletePercentage = 0.33f;

    public bool deleteOnStart = true;

    void Start()
    {
        if (deleteOnStart)
        {
            DeleteChildren();
        }
    }

    [ContextMenu("Delete Random Children")]
    public void DeleteChildren()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        int deleteCount =
            Mathf.RoundToInt(children.Count * deletePercentage);

        for (int i = 0; i < deleteCount; i++)
        {
            if (children.Count == 0)
                return;

            int randomIndex =
                Random.Range(0, children.Count);

            Transform selected =
                children[randomIndex];

            children.RemoveAt(randomIndex);

            Destroy(selected.gameObject);
        }
    }
}