using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBomb : MonoBehaviour
{
    public int damageBoss = 10;
    public int damageFly = 10;
    public Vector2 moveSpeed = new Vector2(10f, 4f);
    Rigidbody2D rb;
    private Animator anim;
    AudioManager audioManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Destroy(gameObject, 5f);
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
            floatingHealthbar.TakeDamage(damageBoss);
            anim.SetTrigger("explode");
            audioManager.PlaySFX(audioManager.bombExplode);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (collision.gameObject.tag == ("GruzMother"))
        {
            floatingHealthbar.TakeDamage(damageBoss);
            anim.SetTrigger("explode");
            audioManager.PlaySFX(audioManager.bombExplode);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (collision.gameObject.tag == ("Enemy"))
        {
            floatingHealthbar.TakeDamage(damageFly);
            anim.SetTrigger("explode");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            audioManager.PlaySFX(audioManager.bombExplode);

            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (collision.gameObject.tag == ("Ground"))
        {
            anim.SetTrigger("explode");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            audioManager.PlaySFX(audioManager.bombExplode);

            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (collision.gameObject.tag == ("Wall"))
        {
            anim.SetTrigger("explode");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            audioManager.PlaySFX(audioManager.bombExplode);

            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
    private void Distances()
    {
        gameObject.SetActive(false);
    }
}
