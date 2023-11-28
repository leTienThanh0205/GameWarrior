using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    public int damage = 10;
    private BoxCollider2D coll;

    private bool hit;
    AudioManager audioManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (collision.CompareTag("Player"))
        {
            hit = true;
            base.OnTriggerEnter2D(collision); //Execute logic from parent script first
            coll.enabled = false;
            health.TakeDamage(damage);
            //anim.SetTrigger("explode");
            if (anim != null)
            {
                anim.SetTrigger("explode"); //When the object is a fireball explode it
                audioManager.PlaySFX(audioManager.arrowExplode);
            }
                
            else
                gameObject.SetActive(false); //When this hits any object deactivate arrow
        }
         if (collision.CompareTag("Wall"))
        {
            hit = true;
            base.OnTriggerEnter2D(collision); //Execute logic from parent script first
            coll.enabled = false;
            if (anim != null)
            {
                anim.SetTrigger("explode"); //When the object is a fireball explode it
                audioManager.PlaySFX(audioManager.arrowExplode);
            }

            else
                gameObject.SetActive(false); //When this hits any object deactivate arrow
        }
        if (collision.CompareTag("Ground"))
        {
            hit = true;
            base.OnTriggerEnter2D(collision); //Execute logic from parent script first
            coll.enabled = false;
            if (anim != null)
            {
                anim.SetTrigger("explode"); //When the object is a fireball explode it
                audioManager.PlaySFX(audioManager.arrowExplode);
            }

            else
                gameObject.SetActive(false); //When this hits any object deactivate arrow
        }

    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}