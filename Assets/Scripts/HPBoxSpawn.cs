using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBoxSpawn : MonoBehaviour
{
    [SerializeField] private GameObject HPBox;
    [SerializeField] private Transform spawnArea;

    // public List<Transform> spawnArea;
    void Start()
    {
        foreach (Transform t in spawnArea)
        {
            var HPBoxSpawn = Instantiate(HPBox, t.position, Quaternion.identity);
        }
    }
}
