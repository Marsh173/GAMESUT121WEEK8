using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thumb_up : MonoBehaviour
{
    PathSystem pathSystem;
    SpriteRenderer spriteRender;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        pathSystem = GameObject.FindGameObjectWithTag("PathSystem").GetComponent<PathSystem>();
        //spriteRender.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

}
