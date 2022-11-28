using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    public int AverageDamage = 0;
    private float difference = 0.17f; // %
    public Score score;

    void Awake()
    {
        damage = Random.Range(System.Convert.ToInt32(AverageDamage - AverageDamage * difference), System.Convert.ToInt32(AverageDamage + AverageDamage * difference));
        StartCoroutine(DestroyEnemyBullet());
    }
    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
        Player player = collision.collider.gameObject.GetComponent<Player>();

        if (!player)
        {
            Destroy(gameObject);
        }
        if (enemy)
        {
            enemy.Health -= damage;
            Debug.Log("Damage: " + damage);
            if (enemy.Health <= 0)
            {
                enemy.Destroy();
                score.ScoreAdd(enemy.Points);
            }
        }
    }
    private void Update()
    {

    }

    private IEnumerator DestroyEnemyBullet()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
