using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    [SerializeField] private GameObject drunk;
    [SerializeField] private int potionPower = 30;
    private void Update()
    {
        if(Time.timeScale == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //on collision playerHP+
        if (collision.gameObject.layer == 10)
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().currentPlayerHealth += potionPower;
            Destroy(this.gameObject);
        }        
    }
 
    private void OnDestroy()
    {
        if(Time.timeScale == 1)
        {
            Instantiate(drunk, transform.position, Quaternion.identity);
        }
    }
}
