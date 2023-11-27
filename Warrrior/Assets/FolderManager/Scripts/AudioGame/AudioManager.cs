using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("-----------Audio-----------")]
    public AudioSource SFX;
    public AudioSource music;
    [Header("-------------SFXPlayer-----------")]
    public AudioClip background;
    public AudioClip jump;
    public AudioClip run;
    public AudioClip walk;
    public AudioClip hurt;
    public AudioClip swordAttack;
    public AudioClip rangedAttack;
    public AudioClip death;
    public AudioClip dash;
    public AudioClip addHealth;
    public AudioClip gameOver;
    public AudioClip bombExplode;
    public AudioClip arrowExplode;
    [Header("-------------SFXEnemy-----------")]
    public AudioClip enemySwordAttack;
    public AudioClip enemyFireAttack;
    public AudioClip fireExplode;
    public AudioClip enemyHurt;
    public AudioClip enemyDie;
    public AudioClip enemyBullet;
    [Header("-------------SFXTrap-----------")]
    public AudioClip sow;
    public AudioClip fireTrap;
    public AudioClip arrowTrap;
    public AudioClip arrowTrapExplode;
    [Header("-------------Items-----------")]
    public AudioClip health;
    public AudioClip coint;
    public AudioClip diamond;
    public AudioClip arrow;
    void Start()
    {
        music.clip = background;
        music.Play();
    }
    

    // Update is called once per frame
    public void PlaySFX(AudioClip clipsFX)
    {
        SFX.PlayOneShot(clipsFX);
    }
}
