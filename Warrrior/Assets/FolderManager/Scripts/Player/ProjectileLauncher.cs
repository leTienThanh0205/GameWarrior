using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProjectileLauncher : MonoBehaviour
{
    //arrow
    public GameObject projectilePrefab;
    public Transform launcherPoint;
    //bomb
    public Transform bombPoint;
    public GameObject bombPrefabs;
    AudioManager audioManager;
    [Header("Timer")]
    public GameObject timerObjBomb;
    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;
    public int Duration;
    private int remainingDuration;
    private bool Pause;
    ItemController itemController;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        itemController = GetComponent<ItemController>();

    }
    private void Start()
    {
        timerObjBomb.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //BombProtectile();
            //timerSkill.OnClickTimer();
            OnClickTimer();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }
    public void OnClickTimer()
    {
        timerObjBomb.SetActive(true);
        // gameObject.SetActive(true);
        if (remainingDuration <= 0 && itemController.numberBomb > 0)
        {

            Being(Duration);
            BombProtectile();
        }
    }


    private void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            if (!Pause)
            {
                uiText.text = $"{remainingDuration % 60:0}";
                // uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        timerObjBomb.SetActive(false);
        OnEnd();
    }

    private void OnEnd()
    {
        print("End");
    }
    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launcherPoint.position, projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;
            audioManager.PlaySFX(audioManager.rangedAttack);
        //flip
        projectile.transform.localScale = new Vector3(
            origScale.x * transform.localScale.x > 0 ? 1 : -1,
            origScale.y,
            origScale.z
            );
        
    }
    public void BombProtectile()
    {
        ///bomb
        GameObject projectBomb = Instantiate(bombPrefabs, bombPoint.position, bombPrefabs.transform.rotation);
        Vector3 origScaleBomb = projectBomb.transform.localScale;
        audioManager.PlaySFX(audioManager.dash);

        //flip
        projectBomb.transform.localScale = new Vector3(
            origScaleBomb.x * transform.localScale.x > 0 ? 1 : -1,
            origScaleBomb.y,
            origScaleBomb.z
            );
    }
}
