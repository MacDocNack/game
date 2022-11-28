using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private int damage;
    public int AverageDamage = 0;
    private float difference = 0.17f; // %

    void Awake()
    {
        damage = Random.Range(System.Convert.ToInt32(AverageDamage - AverageDamage * difference), System.Convert.ToInt32(AverageDamage + AverageDamage * difference));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            Destroy(gameObject);
            player.PlayerHP -= damage;
            if (player.PlayerHP <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void Update()
    {
        StartCoroutine(DestroyEnemyBullet());
    }

    private IEnumerator DestroyEnemyBullet()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
}
