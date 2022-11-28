using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBox : MonoBehaviour
{
    private int HP = 200;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            player.PlayerHP += HP;
            Destroy(gameObject);
            if (player.PlayerHP > player.PlayerMaxHP)
            {
                player.PlayerHP = player.PlayerMaxHP;
            }
        }
    }
}
