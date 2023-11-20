using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    public float CointSpeed;
    public Transform player;
    private bool readyToMove;
    void Start()
    {

    }

    void Update()
    {
        if (readyToMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                player.transform.position, CointSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            readyToMove = true;
            player = GameObject.FindWithTag("Player").transform;

        }
    }
}
