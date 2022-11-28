using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoboxSpawn : MonoBehaviour
{
    [SerializeField] private GameObject AmmoBox;
    [SerializeField] private Transform spawnArea;

    // public List<Transform> spawnArea;
    void Start()
    {
        foreach (Transform t in spawnArea)
        {
            var AmmoBoxSpawn = Instantiate(AmmoBox, t.position, Quaternion.identity);
        }
    }
}
