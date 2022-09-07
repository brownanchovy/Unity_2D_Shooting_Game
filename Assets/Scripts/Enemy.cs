using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    public CircleCollider2D collider;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
