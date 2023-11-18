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


    TouchingDirection touchingDirection;
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



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
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

        anim.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
    void Update()
    {

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
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
            anim.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger(AnimationStrings.attackTrigger);

        }
    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger(AnimationStrings.rangedAttackTrigger);

        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {
       // damageable.LockVelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
