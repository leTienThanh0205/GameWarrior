using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public Text textHP;
    public Text textCoint;
    public Text textDiamond;
    public Text textCointFinish;
    public Text textDiamondFinish;
    public Text textCointLoss;
    public Text textDiamondLoss;
    public int numberHP = 0;
    private int numberCoint = 0;
    private int numberDiamond = 0;
    public int addHealth = 20;
    private Health healthPlayer;
    public GameObject diamondSystem;
    public GameObject cointSystem;
    AudioManager audioManager;

    public GameObject arrow;
    public int numberArrow = 0;
    public GameObject bomb;
    public int numberBomb = 0;
    public GameObject panelWinGame;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    void Start()
    {
        healthPlayer = GetComponent<Health>();
        arrow.SetActive(false); bomb.SetActive(false);
        panelWinGame.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && numberHP>0 && healthPlayer.currentHealth<100)
        {
            numberHP--;
            healthPlayer.AddHealth(addHealth);
            audioManager.PlaySFX(audioManager.addHealth);
        }
        if(numberBomb>0)
        {
            bomb.SetActive(true);
        }
        if (numberArrow > 0)
        {
            arrow.SetActive(true);
        }
        textHP.text = numberHP.ToString();
        textDiamond.text = numberDiamond.ToString();
        textDiamondFinish.text = numberDiamond.ToString();
        textDiamondLoss.text = numberDiamond.ToString();
        textCointLoss.text = numberCoint.ToString();
        textCointFinish.text = numberCoint.ToString();
        textCoint.text = numberCoint.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.CompareTag("HP"))
        {
            numberHP++;
            //Destroy(collision.gameObject);
        }*/
        if (collision.CompareTag("Arrow"))
        {
            audioManager.PlaySFX(audioManager.arrow);
            numberArrow++;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Bomb"))
        {
            audioManager.PlaySFX(audioManager.arrow);
            numberBomb++;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Coint"))
        {
            numberCoint +=50;

            audioManager.PlaySFX(audioManager.coint);
            Destroy(cointSystem);
        }
        if (collision.CompareTag("Diamond"))
        {
            numberDiamond ++;
            audioManager.PlaySFX(audioManager.diamond);
            Destroy(diamondSystem);
        }
        if (collision.CompareTag("Key"))
        {
            audioManager.PlaySFX(audioManager.finish);
            panelWinGame.SetActive(true);
        }
    }
}
