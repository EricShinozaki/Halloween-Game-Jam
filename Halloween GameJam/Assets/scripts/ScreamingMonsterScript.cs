using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamingMonsterScript : Enemy {

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
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

    if(angle < 200 && angle > 160){
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    } else if (angle > -10 && angle < 10){
        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }
    direction.Normalize();
    movement = direction;
    movement.y = 0;
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
