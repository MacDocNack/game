using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    private int Ammo = 4;
    [SerializeField] private Player player;
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            player.bulletCount += Ammo;
            if (player.bulletCount > player.bulletMax)
            {
                player.bulletCount = player.bulletMax;
            }
            Destroy(gameObject);
        }
    }
    
}
