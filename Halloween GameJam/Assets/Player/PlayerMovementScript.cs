using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform transform;
    private Vector2 input;
    private bool direction;
    [SerializeField]
    private float speed = 0.0f;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        input = Vector2.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    }

    private void FixedUpdate(){
        checkAcceleration();
        if(direction == false){
            transform.Translate(input * Time.deltaTime * speed * -1.0f);
        } else if(direction == true){
            transform.Translate(input * Time.deltaTime * speed);
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
}