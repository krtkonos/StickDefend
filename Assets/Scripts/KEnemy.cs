using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KEnemy : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject kEnemy;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject enemyAttackPoint;
    [SerializeField] private LayerMask playerLayer;
    public Transform player;
    [Header("Enemy")]
    [SerializeField] private int maxHealth = 100;
    public static int kEcurrentHealth = 100;
    //[SerializeField] public int currentHealth = 100;    
    public static int playerLastHealth = 100;
    [SerializeField] public float moveSpeed = 100f;
    [SerializeField] public float fall = 100f;
    [SerializeField] private float distance;
    [SerializeField] private AudioSource eDeath;
    [SerializeField] private AudioSource eStabSFX;
    [SerializeField] private AudioSource KEFStep;
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private float potPos;
    [SerializeField] private float zpos;
    [SerializeField] private GameObject potion;
    Vector3 whereToSpawnPotion;
    [Header("Rays")]
    [SerializeField] private float enemyLaserLength = 1;
    [SerializeField] private int enemyAttackPower = 20;
    [SerializeField] private int damageed = 100;
    [SerializeField] private ParticleSystem burn;


    private Rigidbody2D rb;
    private Vector2 movement;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        kEcurrentHealth = maxHealth;
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        renderers = this.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAttack();     
        rb.rotation = 0;
        FollowPlayer();
        RotateEnemy();
        if (GameObject.Find("Player").GetComponent<PlayerMove>().currentPlayerHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void KETakeDamage(int dmg) //Take damage from player
    {

        GetComponent<PlayerMove>();
        kEcurrentHealth -= dmg;
        if (kEcurrentHealth <= 0)
        {
            eDeath.Play();
            Die();
            Blink();
        }
    }
    private void Die() //Die effect and destroy
    {
        Vector3 scale = transform.localScale;
        Debug.Log("Enemy dead");
        Destroy(kEnemy, 1.5f);
        moveSpeed = 0f;
        enemyAttackPower = 0;
        GetComponent<Animator>().SetTrigger("KEDie");
    }


    private void EnemyAttack() //due to distance play animation of attack, dmg is event in animation
    {
        distance = Vector2.Distance(player.transform.position, kEnemy.transform.position);
        if (distance < 3)
        {
            if (kEcurrentHealth > 0)
            {
                GetComponent<Animator>().SetTrigger("KEnemyStab");
            }
        }

        // when enemy stop and run due to distance
        if (distance <= 2f)
        {
            moveSpeed = 0;
        }
        else
        {
            if (kEcurrentHealth > 0)
            {
                moveSpeed = 5;
            }
        }

    }
    private void FollowPlayer() // Folowing player Gameobject
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
    private void RotateEnemy() //Rotating enemy due to position from player
    {

        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(-0.13f, 0.13f, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(0.13f, 0.13f, 1);
        }
    }


    private void OnDestroy() // spawn potion 3m  from place where enemy died, more far from enemy
    {
        if (Time.timeScale > 0) //Score count 1 point per enemy killed
        {
            GameObject.Find("UIManager").GetComponent<ManageScreen>().score++;
            if (gameObject.transform.localScale.x > 0)
            {
                potPos = transform.position.x + 3;
            }
            else
            {
                potPos = transform.position.x - 3;
            }
            zpos = transform.position.z - 1;
            whereToSpawnPotion = new Vector3(potPos, transform.position.y - 3f, zpos);
            if (Random.value <= 0.1f) //chance of potion drop 10%
            {
                Instantiate(potion, whereToSpawnPotion, Quaternion.identity);
            }
            else
            {
                return;
            }
        }
    }
    private void stabSound()
    {
        eStabSFX.Play();
    }
    private void Blink() // when enemy health is 0 enemy is blinking til hes destroyed
    {
        InvokeRepeating("BlinkTo", 0.1f, 0.2f);
        InvokeRepeating("BlinkBack", 0.2f, 0.2f);
    }
    private void BlinkTo() // to invisible
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = new Color(1, 1, 1, 1);
        }

    }
    private void BlinkBack() //to visible
    {
        for (int j = 0; j < renderers.Length; j++)
        {
            renderers[j].material.color = new Color(1, 1, 1, 0);
        }
    }
    public void KEFootStep() // event in animation - footstep sound
    {
        KEFStep.Play();
    }
    private void Att() //attack player using raycaest - event in animation
    {
        Debug.Log("not playing");
        RaycastHit2D hit = Physics2D.Raycast(enemyAttackPoint.transform.position, Vector2.left, enemyLaserLength, playerLayer);

        if (hit.collider != null)
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().currentPlayerHealth -= enemyAttackPower;
            Debug.Log("Player hit");
        }
        else if (hit.collider == null)
        {
            return;
        }
        else
        {
            Debug.Log("no hit");
            return;
        }
    }
}
