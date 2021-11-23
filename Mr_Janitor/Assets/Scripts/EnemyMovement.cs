using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
     public float speed;
     private Transform target;


 void Start () 
 {
     target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
 }
 
 void Update () 
 {
     
     transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
     
 }


 void OnCollisionEnter2D(Collision2D other) 
 {
     PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

     if (player != null)
     {
	 player.ChangeHealth(-1);
     }

     Destroy(gameObject);
 }
}