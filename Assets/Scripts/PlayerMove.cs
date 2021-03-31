using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private Animator chop;
    [SerializeField] private Transform attackPoint;  
    public Transform basicEnemy;
    public GameObject _basicEnemy;

    [Header("Player")]
    [SerializeField] public int playerMaxHealth = 100;
    [SerializeField] public int currentPlayerHealth = 100;
    public static int playerCurrentHealth = 100;
    [SerializeField] private float attackMove = 35f;    

    public Image healthBar;
    [SerializeField] private AudioSource death;
    [SerializeField] private AudioSource swordSound;

    [Header("PlayerTime")]
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private bool nextAttack;

    [Header("Rays")]
    [SerializeField] private float healthBarWidth;
    [SerializeField] private float laserLength = 50f;
    [SerializeField] private float rayPower = 1000f;
    [SerializeField] public int attackPower = 100;
    [SerializeField] private LayerMask enemyMask;
    [Header("Objects")]
    [SerializeField] private GameObject potion;
    Rigidbody2D rb;
    public Vector3 destination;
    public Vector3 goRight;
    public Vector3 goLeft;
    public float smooth = 2f;
    RaycastHit2D hit;

    public object Layermask { get; private set; }
    public KEnemy kEScript;

    private void Start()
    {
        healthBarWidth = healthBar.rectTransform.rect.width;
        chop = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        nextAttack = true; //when tru, player can attack, after chop or stab nothing, must wait 0,1s to next attack
    }

    private void Update()
    {
        HealthBar();
        TakeDamage();
        posChangeRight();
        posChangeleft();

        transform.position = Vector3.Lerp(transform.position, destination, smooth * Time.deltaTime);
        // developer tools
        if (Input.GetKeyDown("a"))
        {
            RaycastHit2D hit = Physics2D.Raycast(attackPoint.transform.position, Vector2.right, laserLength);
            Debug.DrawRay(attackPoint.transform.position, Vector2.right * laserLength, Color.red);
            if(hit.collider != null)
            {               
                Debug.Log("Hitting: " + hit.collider.name);               
            }
        }
        // developer tools
        if (Input.GetKeyDown("q"))
        {
            currentPlayerHealth -= 50;
        }
        if(currentPlayerHealth > playerMaxHealth)
        {
            currentPlayerHealth = playerMaxHealth;
        }      
    }

    private void Awake()
    {
        destination = transform.position;
    }
    public void GoLeft() // button left side of screen
    {
        if (nextAttack == true)
        {
            Invoke("rayleft", 0.15f);
            destination = goLeft; //move player to left, 

            if (Random.value < 0.5f) // play randomly chop or stab animation
            {
                GetComponent<Animator>().SetTrigger("Chop");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Stab");
            }

            transform.localScale = new Vector3(-0.13f, 0.13f, 1f); // occasionaly torate him to left side
        }      
    }
    public void GoRight() // button right side of screen
    {
        if (nextAttack == true)
        {            
            Invoke("rayRight", 0.15f);
            destination = goRight;

            if (Random.value < 0.5f) // play randomly chop or stab animation
            {
                GetComponent<Animator>().SetTrigger("Chop");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Stab");
            }

            transform.localScale = new Vector3(0.13f, 0.13f, 1f); // occasionaly torate him to left side
        }
    }
    public void TakeDamage()
    {
        
        if (currentPlayerHealth <= 0)
        {
            //death.Play();
            //Death();
        }
    }
    public void TakeDamageFromEnemy(int eDmg) // take damage, in the end solved in a different way, useless method
    {
        GetComponent<Enemy>();
        currentPlayerHealth -= eDmg;        
    }
    private void Death()
    {
        Debug.Log("Player dead");
        GetComponent<Animator>().enabled = false;
    }
    private void HealthBar() // HP bar on the right side
    {
        if (currentPlayerHealth >= 0)
        {
            float rightOffset = -(healthBarWidth - ((healthBarWidth / 100) * currentPlayerHealth));
            healthBar.rectTransform.offsetMax = new Vector2(rightOffset, 0);
        }
    }

    
    private void posChangeRight() // seting the move after click on left or right button
    {        
        goRight = new Vector3(transform.position.x + attackMove, -2.298409f, transform.position.z);

    }
    private void posChangeleft() //seting the move after click on left or right button
    {        
        goLeft = new Vector3(transform.position.x - attackMove, -2.298409f, transform.position.z);

    }
    private void rayRight() // ray after clicking button left or right
    { 
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.transform.position, Vector2.right, laserLength, enemyMask);
        Debug.DrawRay(attackPoint.transform.position, Vector2.right * laserLength, Color.red);       

        if (hit)
        {
            KEnemy kEnemy = hit.transform.GetComponent<KEnemy>();
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            MoustacheEnemy mEnemy = hit.transform.GetComponent<MoustacheEnemy>();
            if (kEnemy != null)
            {
                kEnemy.KETakeDamage(100);
                Debug.Log("Hitting: " + hit.collider.name);
                swordSound.Play();
            }
            else if (enemy !=null)
            {
                enemy.TakeDamage(100);
                Debug.Log("Hitting: " + hit.collider.name);
                swordSound.Play();
            }
            else if (mEnemy != null)
            {
                mEnemy.MTakeDamage(100);
                Debug.Log("Hitting: " + hit.collider.name);
                swordSound.Play();
            }
            else
                return;

        }
        else if (hit.collider == null)
        {
            nextAttack = false;
            Invoke("attOn", 0.3f);
            Debug.Log("do not null");
        }

        else
        {
            return;
        }
    }
    private void rayleft() // ray after clicking button left or right
    {
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.transform.position, Vector2.left, laserLength, enemyMask);
        Debug.DrawRay(attackPoint.transform.position, Vector2.left * laserLength, Color.red);

        if (hit)
        {
            KEnemy kEnemy = hit.transform.GetComponent<KEnemy>();
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            MoustacheEnemy mEnemy = hit.transform.GetComponent<MoustacheEnemy>();
            if (kEnemy != null)
            {
                kEnemy.KETakeDamage(100);
                Debug.Log("Hitting: " + hit.collider.name);
                swordSound.Play();
            }
            else if (enemy != null)
            {
                enemy.TakeDamage(100);
                Debug.Log("Hitting: " + hit.collider.name);
                swordSound.Play();
            }
            else if (mEnemy != null)
            {
                mEnemy.MTakeDamage(100);
                Debug.Log("Hitting: " + hit.collider.name);
                swordSound.Play();
            }
            else
                return;

        }
        else if (hit.collider == null)
        {
            nextAttack = false;
            Invoke("attOn", 0.3f);
            Debug.Log("do not null");
        }

        else
        {
            return;
        }

    }
    private void attOn() // settin nextattack true method
    {
        nextAttack = true;
    }
}
