using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject knifeEnemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform _player;
    [SerializeField] private float randX;
    Vector2 whereToSpawn;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float nextSpawn = 0.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            if (Random.value < 0.5f)
            {
                randX = player.transform.position.x - 12;
            }

            else
            {
                randX = player.transform.position.x + 12f;
            }

            nextSpawn = Time.time + spawnRate;
            //randX = Random.Range(player.transform.position.x -10, player.transform.position.x + 10f);
            whereToSpawn = new Vector2(randX, transform.position.y);

            Instantiate(knifeEnemy, whereToSpawn, Quaternion.identity);
        }
        if (knifeEnemy.transform.position.x < _player.transform.position.x)
        {
            knifeEnemy.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

    }
}
