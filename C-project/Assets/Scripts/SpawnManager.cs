using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> Sectors = new List<GameObject>();

    private int postionZ = 50;

    private void Start()
    {
        StartCoroutine(SpawnSector());
    }

    IEnumerator SpawnSector()
    {
        while (postionZ <= 1750)
        {
            Instantiate(Sectors[Random.Range(0, Sectors.Count)], new Vector3(0, 0, postionZ), new Quaternion());
            postionZ += 100;
            yield return new WaitForSeconds(10);
        }
    }
}