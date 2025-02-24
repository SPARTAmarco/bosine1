
using UnityEngine;

public class controller : MonoBehaviour
{

    private float movementInputDirection;

    private bool isFacingRight = true;
    private bool isWalking ;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canJump;
    private bool isCrouching=false;
    private float movementSpeed1;


    private Rigidbody2D rb;
    private Animator animator;

    private int amountOfJumpsLeft;
    private int facingDirection = 1;

    public int amountOfJumps = 1;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float crouchSpeed = 1.0f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    bool wallJumping;

    public Transform groundCheck;
    public Transform wallCheck;

    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    //Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        amountOfJumpsLeft=amountOfJumps;
        movementSpeed1=movementSpeed;

    }

    //Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        Crouch();
    }


    private void CheckMovementDirection()
    {    
        if(isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if(!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if(rb.linearVelocity.x  != 0)
        {
            isWalking=true;
        } else {
            isWalking = false;
        }
    }

    //Spostare il personaggio a destra o sinistra
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate (0.0f, 180.0f, 0.0f);
        facingDirection *= -1; 
    }
    //

    private void CheckIfCanJump()
    {
        if(isGrounded &&rb.linearVelocity.y <=0 || isWallSliding)
        {
            amountOfJumpsLeft = amountOfJumps;
        }
        if(amountOfJumpsLeft <=0)
        {
            canJump= false;
        }
        else{
            canJump=true;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("isWallSliding", isWallSliding);
        animator.SetBool("crouch", isCrouching);
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurronding();
        if(wallJumping)
        {
            rb.linearVelocity=new Vector2(-movementInputDirection * wallJumpForce.x, wallJumpForce.y);
        }
        else{
            rb.linearVelocity = new Vector2(movementInputDirection*movementSpeed, rb.linearVelocity.y);
        }
    }

    private void CheckSurronding()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);
    }

    private void CheckIfWallSliding()
    {
        if(isTouchingWall && !isGrounded && rb.linearVelocity.y <0)
        {
            isWallSliding=true;
        } 
        else{
            isWallSliding=false;
        }
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            jump();
        }

        if(Input.GetButtonUp("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * variableJumpHeightMultiplier);
        }
    }

    private void ApplyMovement()
    {
        rb.linearVelocity= new Vector2(movementSpeed * movementInputDirection, rb.linearVelocity.y);
       if(!isGrounded && !isWallSliding && movementInputDirection!=0)
       {
            Vector2 forceToAdd = new Vector2 (movementForceInAir * movementInputDirection, 0);
            rb.AddForce(forceToAdd);

            if(Mathf.Abs(rb.linearVelocity.x) > movementSpeed)
            {
                rb.linearVelocity = new Vector2 (movementSpeed * movementInputDirection, rb.linearVelocity.y);
            }
        }
        else if(!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * airDragMultiplier, rb.linearVelocity.y);
        }

        if(isWallSliding)
        {
            if(rb.linearVelocity.y < -wallSlideSpeed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
            }
        }
    }

    private void jump()
    {
        if(canJump && !isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
        else if(isWallSliding && canJump)
        {
            amountOfJumpsLeft--;
            wallJumping =true;
            Invoke("stopWallJump", wallJumpDuration);
        }
    }

    void stopWallJump()
    {
        wallJumping = false;
    }

    private void Crouch()
    {
        if(Input.GetButtonDown("Crouch"))
        {
            isCrouching=true;
        } else if(Input.GetButtonUp("Crouch"))
        {
            isCrouching=false;
        }
        if(isCrouching && movementSpeed==movementSpeed1)
        {
            movementSpeed*=crouchSpeed;
        } 
        else{
            movementSpeed=movementSpeed1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}