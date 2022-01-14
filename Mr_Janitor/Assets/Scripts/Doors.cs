using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    public float tpX = 20f;
    public float tpY = -5f;
    int i;

    public Collider2D boundsTrigger;

    List<GameObject> internalGameObjects = new List<GameObject>();

    Collision2D collision;

    Animator animator;

    public GameObject[] items;

    public void Start()
    {
        animator = GetComponent<Animator>();

        GameObject[] itemsInsideCollider = GetItemsInsideCollider();
        Debug.Log("There are " + itemsInsideCollider.Length + " items in the collider!");

    }

    public GameObject[] GetItemsInsideCollider()
    {
        

        for (i = 0; i < items.Length; i++)
        {
            if (boundsTrigger.bounds.Contains(items[i].transform.position))
            {
                internalGameObjects.Add(items[i]);
            }
        }

        for (i = 0; i > items.Length; i--)
        {
            
        }

        return internalGameObjects.ToArray();
    }

    void Update()
    {

    }

    public void DoorHit()
    {
        collision.gameObject.GetComponent<Transform>().position = new Vector2(tpX, tpY);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

        if (items.Length == 0)
        {
            //GetComponent<TPBlackPlay>().Play(animator.SetTrigger("TPScreen"));
            collision = other;
            Invoke("DoorHit", 2);
        }

    }
}