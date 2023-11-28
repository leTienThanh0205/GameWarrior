using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        //Attack();
    }
    public void Attack()
    {
        if (cooldownTimer >= attackCooldown)
        {
            // SoundManager.instance.PlaySound(arrowSound);
            arrows[FindArrow()].transform.position = firePoint.position;
            audioManager.PlaySFX(audioManager.arrowTrap);
            arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
            cooldownTimer = 0;
        }
    }
}