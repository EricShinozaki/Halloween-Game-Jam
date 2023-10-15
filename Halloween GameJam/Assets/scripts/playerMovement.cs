using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Transform transform;
    private Vector2 input;
    private bool direction;
    [SerializeField]
    private float speed = 0.0f;
    public float jumpPower;
    private bool canJump = true;
    private bool wpress = false;
    private bool leftClick = false;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;

    public Animator animator;

    //public int health;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int damage; 
    public float attackRate;
    float nextAttackTime = 0f;

    public GameObject attackArea;

    void Start()
    {
        input = Vector2.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

        //healthSystem healthSystem = new healthSystem(health);

        animator = GetComponent<Animator>();
        attackArea = GameObject.FindWithTag("area");
        attackArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime){
            if(Input.GetMouseButtonDown(0)){
                leftClick = true;
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if(leftClick){
            attack();
            Debug.Log("Attack");
        }
        }
        animator.SetBool("Attack", false);
        if(input.x == 0){
            speed = 0f;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.W)){
            wpress = true;
        }
        getInputDirection();
    }

    private void FixedUpdate(){
        checkAcceleration();

        if(direction == false){
            transform.Translate(input * Time.deltaTime * speed * -1.0f);
        } else if(direction == true){
            transform.Translate(input * Time.deltaTime * speed);
        }

        if(wpress && canJump){
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            canJump = false;
            wpress = false;
        }
 
    }

    private void getInputDirection(){
        if(Input.GetKeyDown(KeyCode.A)){
            direction = false;
            animator.SetBool("direction", true);
            attackArea.transform.position = new Vector3((transform.position.x - 1), (transform.position.y+0.3f), 0f);
        } else if(Input.GetKeyDown(KeyCode.D)){
            direction = true;
            animator.SetBool("direction", false);
            attackArea.transform.position = new Vector3((transform.position.x + 1), (transform.position.y+0.3f), 0f);
        } if(Input.GetMouseButtonDown(0)){
            animator.SetBool("Attack", true);
        }
    }

    private void checkAcceleration(){
        if(direction == false && speed >= -5){
            speed -= 0.25f;
        } else if(direction == true  && speed <= 5){
            speed += 0.25f;
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")){
            canJump = true;
        }
        Debug.Log("Hit ground");
    }

    void attack(){
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        attackArea.SetActive(true);

        //Damage enemies in range
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<Enemy>().takeDamage(damage);
        }

        animator.SetBool("Attack", false);
        leftClick = false;
        attackArea.SetActive(false);
    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
