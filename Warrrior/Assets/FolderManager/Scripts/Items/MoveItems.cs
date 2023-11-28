using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItems : MonoBehaviour
{
    public float speed = 1f;
    bool moveCoint;
    public GameObject target;
    AudioManager audioManager;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("ToPoint");
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (moveCoint)
        {
            transform.position = Vector3.Lerp(transform.position,
                target.transform.position, speed * Time.deltaTime);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemController itemController = collision.GetComponent<ItemController>();
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            moveCoint = true;
            itemController.numberHP++;
            audioManager.PlaySFX(audioManager.arrow);
            Destroy(gameObject, 1.5f);
        }
        
    }
}
