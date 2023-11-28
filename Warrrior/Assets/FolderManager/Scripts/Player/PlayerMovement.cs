using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class PlayerMovemt : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
   // Damageable damageable;

    Vector2 moveInput;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;
    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    private float dasdingTime = 0.2f;
    private float dashingColldown = 1f;
    [SerializeField] private TrailRenderer tr;
    [Header("climp")]
    private bool esSuelo;
    [SerializeField] private float velocidadEscalar;
    private CapsuleCollider2D box;
    private float gravedadInicial;
    private bool escalando;

    TouchingDirection touchingDirection;
    Timer timer;
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirection.IsOnWall)
                {
                    if (touchingDirection.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        //air Move
                        return airWalkSpeed;
                    }

                }
                else
                {
                    //idle speed is 0
                    return 0;
                }
            }
            else
            {
                //movement locked;
                return 0;
            }

        }
    }
    [SerializeField]
    private bool _ismoving = false;
    public bool IsMoving
    {
        get
        {
            return _ismoving;
        }
        set
        {
            _ismoving = value;
            anim.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            anim.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    private bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    public bool CanMove
    {
        get
        {
            return anim.GetBool(AnimationStrings.canMove);
        }
    }
    public bool IsAlive
    {
        get
        {
            return anim.GetBool(AnimationStrings.isAlive);
        }
    }
    AudioManager audioManager;

   


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<CapsuleCollider2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        timer = GetComponent<Timer>();
        gravedadInicial = rb.gravityScale;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // damageable = GetComponent<Damageable>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
       // if (!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        if (isDashing)
        {
            rb.velocity = new Vector2(moveInput.x * dashingPower, rb.velocity.y);
            return;
        }
        Escalar();

       // anim.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
    void Update()
    {
        ///dash
        if (isDashing)
        {
            rb.velocity = new Vector2(moveInput.x * dashingPower, rb.velocity.y);
            return;
        }
        if (Input.GetKeyDown(KeyCode.C) && canDash)
        {
            StartCoroutine(Dash());
            audioManager.PlaySFX(audioManager.dash);
        }

        if (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon)
        {
            anim.SetFloat(AnimationStrings.yVelocity, Mathf.Sign(rb.velocity.y));

        }
        else
        {
            anim.SetFloat(AnimationStrings.yVelocity, 0);
        }
    }
    //climp

    private void Escalar()
    {
        if ((moveInput.y != 0 || escalando) && (box.IsTouchingLayers(LayerMask.GetMask("Climp"))))
        {
            Vector2 velocidadSubida = new Vector2(rb.velocity.x, moveInput.y * velocidadEscalar);
            rb.velocity = velocidadSubida;
            rb.gravityScale = 0;
            escalando = true;

        }
        else
        {
            rb.gravityScale = gravedadInicial;
            escalando = false;

        }
        if (esSuelo)
        {
            escalando = false;
        }
        anim.SetBool("climp", escalando);
    }
    //Dash
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        tr.emitting = true;
        yield return new WaitForSeconds(dasdingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingColldown);
        canDash = true;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
            audioManager.PlaySFX(audioManager.walk);
        }
        else
        {
            IsMoving = false;
        }

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //face the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            //face the left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
            audioManager.PlaySFX(audioManager.run);
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.IsGrounded && CanMove)
        {
            audioManager.PlaySFX(audioManager.jump);
            anim.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            audioManager.PlaySFX(audioManager.swordAttack);
            anim.SetTrigger(AnimationStrings.attackTrigger);

        }
    }
   /* public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // timer.OnClickTimer();
             anim.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }*/
    public void OnHit(int damage, Vector2 knockback)
    {
       // damageable.LockVelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
