using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public int damage2 = 30;
    public int damage3 = 30;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    Rigidbody2D rb;
    public Vector2 knockBack = new Vector2(0, 0);
    private Animator anim;
    AudioManager audioManager;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

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
            floatingHealthbar.TakeDamage(damage3);
            anim.SetTrigger("explode");
            audioManager.PlaySFX(audioManager.arrowExplode);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (collision.gameObject.tag == ("GruzMother"))
        {
            floatingHealthbar.TakeDamage(damage3);
            anim.SetTrigger("explode");
            audioManager.PlaySFX(audioManager.arrowExplode);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (collision.gameObject.tag == ("Enemy"))
        {
            floatingHealthbar.TakeDamage(damage2);
            anim.SetTrigger("explode");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            audioManager.PlaySFX(audioManager.arrowExplode);

            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if(collision.gameObject.tag == ("Ground"))
        {
            anim.SetTrigger("explode");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            audioManager.PlaySFX(audioManager.arrowExplode);

        }
        if (collision.gameObject.tag == ("Wall"))
        {
            anim.SetTrigger("explode");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            audioManager.PlaySFX(audioManager.arrowExplode);

            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
    private void Distances()
    {
        gameObject.SetActive(false);
    }
}
