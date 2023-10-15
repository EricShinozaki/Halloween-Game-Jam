using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMonsterController : MonoBehaviour {

public Transform player;
private Rigidbody2D rigidbody;
public Animator animator;
private Vector3 movement;
public float moveSpeed;
private Vector3 direction;

void Start(){
    rigidbody = GetComponent<Rigidbody2D>();
    movement = Vector3.zero;
    animator = GetComponent<Animator>();
}

void Update(){
    Vector3 scale = transform.localScale;
    direction = player.position - transform.position;
    Debug.Log(direction); //Have monster move towards player if direction < 6
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
    Debug.Log(angle);

    if(angle < 200 && angle > 160){
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Debug.Log("working");
    } else if (angle > -10 && angle < 10){
        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        Debug.Log("working");
    }
    direction.Normalize();
    movement = direction;
}

private void FixedUpdate(){
    if(direction.magnitude < 4){
        rigidbody.velocity = movement * moveSpeed;
        animator.SetBool("enterSight", true);
        animator.SetBool("leaveSight", false);
    } else {
        animator.SetBool("enterSight", false);
        animator.SetBool("leaveSight", true);
    }
}
}
