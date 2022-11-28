using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public Score score;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Bullet>() != null) score.ScoreReduce();
        Destroy(collision.gameObject);
    }
}
