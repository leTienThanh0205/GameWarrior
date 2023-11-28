using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
    private Animator animator;
    public Slider healthSlider; 
    public GameObject healthbarEnemy;
    public int maxHealth = 100; 
    private int currentHealth; 
    public GameObject effectDie;
    public GameObject effectHurt;
    public GameObject coints;
    AudioManager audioManager;
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        healthbarEnemy.SetActive(false);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 100 && currentHealth >= 60)
        {
            healthbarEnemy.SetActive(true);
            Instantiate(effectHurt, transform.position, Quaternion.identity);
            audioManager.PlaySFX(audioManager.enemyHurt);
            animator.SetTrigger("hurt");
        }
        else if (currentHealth < 60 && currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            Instantiate(effectHurt, transform.position, Quaternion.identity);
            audioManager.PlaySFX(audioManager.enemyHurt);

            healthbarEnemy.SetActive(true);
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthbarEnemy.SetActive(false);
            audioManager.PlaySFX(audioManager.enemyDie);

            animator.SetTrigger("die");
            coints.SetActive(true);
            gameObject.SetActive(false);
            Instantiate(coints, transform.position, Quaternion.identity);
            Instantiate(effectDie, transform.position, Quaternion.identity);
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthSlider.value = healthPercentage;
    }
    private void Distances()
    {
        gameObject.SetActive(false);


    }
}
