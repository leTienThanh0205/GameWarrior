using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBomb : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(10f, 4f);
    Rigidbody2D rb;
    private Animator anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FloatingHealthbar floatingHealthbar = collision.GetComponent<FloatingHealthbar>();
        
        if (collision.gameObject.tag == ("Boss"))
        {
            floatingHealthbar.TakeDamage(damage);
            anim.SetTrigger("explode");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (collision.gameObject.tag == ("Ground"))
        {
            anim.SetTrigger("explode");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
    private void Distances()
    {
        gameObject.SetActive(false);
    }
}
