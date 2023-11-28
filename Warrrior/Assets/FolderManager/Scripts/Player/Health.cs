using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthSlider; 
    public Image fillHealthbar;
    public int maxHealth = 100; 
    public int currentHealth; 
    public GameObject effectDie;
    public GameObject effectHurt;
    public GameObject effectHealth;
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject panelLoss;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        panelLoss.SetActive(false);
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 100 && currentHealth >= 60)
        {
            fillHealthbar.color = Color.green;
            anim.SetBool("isAlive", true);
            anim.SetTrigger("hit");
            Instantiate(effectHurt, transform.position, Quaternion.identity);
            audioManager.PlaySFX(audioManager.hurt);

        }
        else if (currentHealth < 60 && currentHealth > 0)
        {
            fillHealthbar.color = Color.red;
            anim.SetBool("isAlive", true);
            anim.SetTrigger("hit");
            Instantiate(effectHurt, transform.position, Quaternion.identity);
            audioManager.PlaySFX(audioManager.hurt);

        }
        if (currentHealth <= 0)
        {
            currentHealth = 0; // hp always > 0
            anim.SetBool("isAlive",false);
            
            Deactivate();
            rb.transform.localScale = Vector3.zero;
            MusicDie();
        }
        UpdateHealthBar();
    }
    public void AddHealth(int health)
    {
        currentHealth += health;
        if (currentHealth ==99 && currentHealth >= 60)
        {
            fillHealthbar.color = Color.green;
        }
        else if (currentHealth < 60 && currentHealth > 0)
        {
            fillHealthbar.color = Color.red;
        }
        if (currentHealth >= 100)
        {
            currentHealth = 100;
            fillHealthbar.color = Color.green;
        }
        EffectHealth();
        UpdateHealthBar();
    }

    // update Health
    void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthSlider.value = healthPercentage;
    }
    private void Deactivate()
    {
       // gameObject.SetActive(false);
        Instantiate(effectDie, transform.position, Quaternion.identity);
        Invoke("ShowLoss", 0.5f);
    }
    private void ShowLoss()
    {
        Time.timeScale = 0;

        panelLoss.SetActive(true);
    }
    private void EffectHealth()
    {
        Instantiate(effectHealth, transform.position, Quaternion.identity);

    }
    void MusicDie()
    {
        audioManager.PlaySFX(audioManager.death);
        audioManager.music.Stop();
        audioManager.PlaySFX(audioManager.gameOver);
    }
}
