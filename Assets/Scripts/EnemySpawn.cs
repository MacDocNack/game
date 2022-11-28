using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform player;

    [SerializeField] private Transform spawnArea;

    // public List<Transform> spawnArea;
    void Start()
    {
        foreach (Transform t in spawnArea)
        {
            var enemySpawn = Instantiate(enemy, t.position, Quaternion.identity);
            enemySpawn.GetComponentInChildren<Enemy>().target = player;
        }
    }
}
