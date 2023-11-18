using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textHP,textArrow;
    private int numberHP = 0;
    private int numberArrow = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            numberHP--;
            Debug.Log("huy HP");
        }else if (Input.GetKeyDown(KeyCode.M))
        {
            numberArrow--;
            Debug.Log("huy HP");

        }
        if (numberHP < 0)
        {
            Debug.Log("Die CMNR");
        }
        textHP.text = numberHP.ToString();
        textArrow.text = numberArrow.ToString();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HP"))
        {
            numberHP++;
            Debug.Log("Eat HP");
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Arrow"))
        {
            numberArrow++;
            Debug.Log("Eat Arrow");
            Destroy(collision.gameObject);

        }
    }
}
