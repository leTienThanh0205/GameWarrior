using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TimerSkill : MonoBehaviour, IPointerClickHandler
{
    public GameObject timerObjArrow;
    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }

    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;
    private Animator anim;

    public int Duration;

    private int remainingDuration;

    private bool Pause;
    ItemController itemController;
    private void Awake()
    {
        itemController = GetComponent<ItemController>();


    }

    private void Start()
    {
        timerObjArrow.SetActive(false);
       // gameObject.SetActive(false);
        anim = GetComponent<Animator>();
    }
    public void OnClickTimer()
    {
        timerObjArrow.SetActive(true);
       // gameObject.SetActive(true);
        if (remainingDuration <= 0 && itemController.numberArrow>0)
        {
            Being(Duration);
            anim.SetTrigger(AnimationStrings.rangedAttackTrigger);
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
        timerObjArrow.SetActive(false);
        OnEnd();
    }

    private void OnEnd()
    {
        print("End");
    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnClickTimer();
            // timer.OnClickTimer();
            /*anim.SetTrigger(AnimationStrings.rangedAttackTrigger);*/
        }
    }
}

