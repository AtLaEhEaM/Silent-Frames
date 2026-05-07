using UnityEngine;
using System.Collections.Generic;

public class LeafRandomness : MonoBehaviour
{
    public List<GameObject> leaves;
    public Vector2 spawnAmount;
    public Vector2 spawnHeight;


    public Vector2 xRange;
    public Vector2 zRange;

    void Start()
    {
        RandomizeFlower();
        RandomizePosition();
    }

    void RandomizeFlower()
    {
        foreach (GameObject leaf in leaves)
        {
            leaf.SetActive(false);
        }

        int amount = Random.Range((int)spawnAmount.x, (int)spawnAmount.y + 1);

        List<GameObject> available = new List<GameObject>(leaves);

        for (int i = 0; i < amount && available.Count > 0; i++)
        {
            int randomLeaf = Random.Range(0, available.Count);

            GameObject leaf = available[randomLeaf];

            leaf.SetActive(true);

            Vector3 pos = leaf.transform.localPosition;
            //pos.y = Random.Range(spawnHeight.x, spawnHeight.y);
            //leaf.transform.localPosition = pos;

            available.RemoveAt(randomLeaf);
        }
    }

    public void RandomizePosition()
    {
        Vector3 pos = transform.localPosition;

        float randomX = Random.Range(xRange.x, xRange.y);
        float randomZ = Random.Range(zRange.x, zRange.y);

        pos.x += randomX;
        pos.z += randomZ;

        transform.localPosition = pos;
    }
}