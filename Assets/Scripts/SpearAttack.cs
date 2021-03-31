using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAttack : MonoBehaviour
{
    public static int basicEnemyAttackPower = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(Enemy.currentHealth >= 0)
            {
                Debug.Log("spear hit detected");
                PlayerMove.playerCurrentHealth -= basicEnemyAttackPower;
            }
            
            
        }
    }
    
}
