using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicEnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject basicEnemy;
    [SerializeField] private GameObject kEnemy;
    [SerializeField] private GameObject mEnemy;
    [SerializeField] private GameObject player;

    [SerializeField] private Transform _player;
    [SerializeField] private float randX;
    [SerializeField] private float chance = 100f;
    [SerializeField] private float chance2 = 0;
    private float chance3 = 0f;
    Vector2 whereToSpawn;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] public float maxSpawnRate = 2f;
    [SerializeField] private float minSpawnRate = 0.7f;
    [SerializeField] private float nextSpawn = 0.0f;
    [SerializeField] private float vave;
    [SerializeField] private Text timee;
    [SerializeField] private bool pause = true;
    [SerializeField] private GameObject prepareText;
    [SerializeField] private GameObject prepareLastText;
    [SerializeField] private GameObject textcanvas;



    void Start()
    {
        //text telling about next waves
        prepareText.SetActive(false);
        prepareLastText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        waves(); //start and stop waves
        waveText(); //call a wave text
        timee.text = "" +  Math.Round((vave), 2, MidpointRounding.AwayFromZero);
        if (Time.timeScale == 0)
        {
            vave = 0;
            prepareText.SetActive(false);
            prepareLastText.SetActive(false);
            spawnRate = maxSpawnRate;
        }
        chance3 = chance + chance2;
        typeSpawning();
    }
    private void gen() //original method useless now
    {
        if (Time.time > nextSpawn) 
        {
            if (UnityEngine.Random.value < 0.5f)
            {
                randX = player.transform.position.x - 10;
            }

            else
            {
                randX = player.transform.position.x + 10f;
            }

            nextSpawn = Time.time + spawnRate;
            whereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(basicEnemy, whereToSpawn, Quaternion.identity);
            
            if (GameObject.Find("Player").GetComponent<PlayerMove>().currentPlayerHealth <= 0)
            {
                gameObject.SetActive(false);
            }
            if (GameObject.Find("UIManager").GetComponent<ManageScreen>().score > 5)
            {
                spawnRate -= 0.2f;
            }
            else if (GameObject.Find("UIManager").GetComponent<ManageScreen>().score > 11)
            {
                spawnRate -= 0.2f;
            }
            else if (GameObject.Find("UIManager").GetComponent<ManageScreen>().score > 14)
            {
                spawnRate -= 0.2f;
            }
            else if (GameObject.Find("UIManager").GetComponent<ManageScreen>().score > 17)
            {
                spawnRate -= 0.2f;
            }
            else if (GameObject.Find("UIManager").GetComponent<ManageScreen>().score > 20)
            {
                spawnRate -= 0.2f;
            }
            if (spawnRate < minSpawnRate) 
            {
                spawnRate = minSpawnRate;
            }
        }
    }
    void generate(GameObject enemyType) //generate one of three types of enemy
    {        
        
            if (Time.time > nextSpawn) //uprising spawnrate per better score randomly spawn enemies on the left or on the right
        {
                if (UnityEngine.Random.value < 0.5f)
                {
                    randX = player.transform.position.x - 10;
                }

                else
                {
                    randX = player.transform.position.x + 10f;
                }

                nextSpawn = Time.time + spawnRate;
                whereToSpawn = new Vector2(randX, transform.position.y);
                Instantiate(enemyType, whereToSpawn, Quaternion.identity);

                if (GameObject.Find("Player").GetComponent<PlayerMove>().currentPlayerHealth <= 0)
                {
                    gameObject.SetActive(false);
                }
                if (GameObject.Find("UIManager").GetComponent<ManageScreen>().score > 5)
                {
                    spawnRate -= 0.2f;
                }
                else if (GameObject.Find("UIManager").GetComponent<ManageScreen>().score > 11)
                {
                    spawnRate -= 0.2f;
                }
                else if (GameObject.Find("UIManager").GetComponent<ManageScreen>().score > 14)
                {
                    spawnRate -= 0.2f;
                }
                else if (GameObject.Find("UIManager").GetComponent<ManageScreen>().score > 17)
                {
                    spawnRate -= 0.2f;
                }
                else if (GameObject.Find("UIManager").GetComponent<ManageScreen>().score > 20)
                {
                    spawnRate -= 0.2f;
                }
                if (spawnRate < minSpawnRate) //set min spawnrate
            {
                    spawnRate = minSpawnRate;
                }
            }     
    }
    void waves() //generates enemies per wave
    {        
        vave += Time.deltaTime;

        if (pause == true)
        {
            if (UnityEngine.Random.value < chance/100)
            {
                generate(kEnemy);
            }
            else if (UnityEngine.Random.value > chance/100 && UnityEngine.Random.value < chance3/100)
            {
                generate(basicEnemy);
            }
            else
            {
                generate(mEnemy);
            }                        
        }
        

        if (vave > 10f && vave < 15)
        {
            pause = false;
        }
        else if (vave > 25 && vave < 30)
        {
            pause = false;
        }
        else if (vave > 40 && vave < 45)
        {
            pause = false;
        }
        else if (vave > 55 && vave < 60)
        {
            pause = false;
        }
        else if (vave > 70 && vave < 75)
        {
            pause = false;
        }
        else if (vave > 85 && vave < 90)
        {
            pause = false;
        }
        else
        {
            pause = true;
        } 
    }
    private void waveText()
    {

        if (vave > 12.5f && vave < 15)
        {
            prepareText.SetActive(true);
        }
        else if (vave > 27.5f && vave < 30)
        {
            prepareText.SetActive(true);
        }
        else if (vave > 42.5f && vave < 45)
        {
            prepareText.SetActive(true);
        }
        else if (vave > 57.5f && vave < 60)
        {
            prepareText.SetActive(true);
        }
        else if (vave > 72.5f && vave < 75)
        {
            prepareText.SetActive(true);
        }
        else if (vave > 87.5f && vave < 90)
        {
            prepareLastText.SetActive(true);
        }
        else
        {
            prepareText.SetActive(false);
            prepareLastText.SetActive(false);
        }        
    }
    private void typeSpawning()
    {
        if(vave < 15)
        {
            chance = 100;
            chance2 = 0;
        }
        else if(vave > 15 && vave < 30)
        {
            chance = 70;
            chance2 = 30;
        }
        
        else if(vave > 30 && vave < 45)
        {
            chance = 55;
            chance2 = 35;
        }
        else if(vave > 45 && vave < 60)
        {
            chance = 40;
            chance2 = 40;
        }
        else if(vave > 60 && vave < 75)
        {
            chance = 30;
            chance2 = 40;
        }
        else if(vave > 75 && vave < 90)
        {
            chance = 33;
            chance2 = 33;
        }
        else
        {
            chance = 20;
            chance2 = 30;
        }
    }    
}
