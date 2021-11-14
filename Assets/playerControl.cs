using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    BoxCollider2D box;
    SpriteRenderer spriteDog;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        spriteDog = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("like"))
        {
            Debug.Log("Meet");
            spriteDog.color = Color.green;
            
        }
        else if (collision.CompareTag("dislike"))
        {
            Debug.Log("OMG");
            spriteDog.color = Color.red;
        }
    }

}
