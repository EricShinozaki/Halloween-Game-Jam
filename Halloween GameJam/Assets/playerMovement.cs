using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    public Transform transform;
    private Vector2 input;
    private bool direction;
    [SerializeField]
    private float speed = 0.0f;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    public float jumpPower;
    private bool canJump = true;
    private bool wpress = false;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;

    public Animator animator;
    void Start()
    {
        input = Vector2.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.x == 0){
            speed = 0f;
        }
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        getInputDirection(); 
        if(Input.GetKeyDown(KeyCode.W)){
            wpress = true;
        }
    }

    private void FixedUpdate(){
        checkAcceleration();
        if(direction == false){
            transform.Translate(input * Time.deltaTime * speed * -1.0f);
        } else if(direction == true){
            transform.Translate(input * Time.deltaTime * speed);
        }
        if(wpress && canJump){
            Debug.Log("Jump key pressed.");
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            canJump = false;
            wpress = false;
        }
    }

    private void getInputDirection(){
        if(Input.GetKeyDown(KeyCode.A)){
            direction = false;
            spriteRenderer.sprite = spriteLeft;
        } else if(Input.GetKeyDown(KeyCode.D)){
            direction = true;
            spriteRenderer.sprite = spriteRight;
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
            Debug.Log("Landed on the ground");
        }
    }
}
