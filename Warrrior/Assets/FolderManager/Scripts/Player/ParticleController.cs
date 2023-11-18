using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] ParticleSystem movementParticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRb;
    bool isGround;
    float counter;
   // [Header("")]
    //[SerializeField] ParticleSystem fallParticle;
   // [SerializeField] ParticleSystem touchParticle;

    //AudioManager audioManager;
    private void Start()
    {
       // touchParticle.transform.parent = null;
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();


    }
    private void Update()
    {
        counter += Time.deltaTime;
        if (isGround && playerRb.velocity.x !=0)
        {
            movementParticle.Play();
            Debug.Log("AHIHIHI");
            counter = 0;
            /*if (counter > dustFormationPeriod)
            {
               
            }*/
        }
    }
    public void PlayTouchParticle(Vector2 pos)
    {
       // touchParticle.transform.position = pos;
        //audioManager.PlaySFX(audioManager.wallTouch);
       // touchParticle.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGround = true;
           // movementParticle.Play();
            Debug.Log("Ground: dc cham");
            //fallParticle.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
