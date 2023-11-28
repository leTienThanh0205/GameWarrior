using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    private Animator anim;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position,transform.position);
        if(distanceFromPlayer < lineOfSite  && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }else if(distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            // Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            //nextFireTime = Time.time + fireRate;
            
            anim.SetBool("attack",true);
        }
        else
        {
            anim.SetBool("attack",false);
        }
        Flip();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
    private void Flip()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,180, 0);

        }
    }
    private void BeeFlyAttack()
    {
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        audioManager.PlaySFX(audioManager.enemyBullet);
        nextFireTime = Time.time + fireRate;

    }
}
