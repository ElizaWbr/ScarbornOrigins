using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerMovement : Inputs
{

    [Header("Player")]
    public Rigidbody2D currentRigidbody;

    [Header("Health")]
    public HealthBase healthBase;

    [Header("Player movement")]
    public float speed = 15;
    public float runSpeed = 30;
    public float jumpForce = 23;
    public float jumpDurationInSeconds = 1f;
    public float movementFriction = .8f;

    [Header("Animation Setup")]
    public Animator animator;
    public string runAnimationKey = "Run";
    public string triggerDeath = "Death";

    public float jumpScaleY = 1.2f;
    public float jumpScaleX = 0.8f;
    public float jumpAnimationDuration = .3f;
    public Ease jumpEase = Ease.OutBack;

    public float playerSwipeDuration = .1f;

    private float _currentSpeed;
    private bool _isRunning = false;
    private bool _isJumping = false;


    private void Awake()
    {
        LoadInputs();

        if (healthBase != null)
        {
            healthBase.OnKill += PlayerOnKill;
        }
    }

    private void Update()
    {
        if (healthBase.isDead())
        {
            return;
        }

        CheckPlayerJump();
        CheckPlayerMovement();
    }
    private void OnDestroy()
    {
        DOTween.Kill(currentRigidbody.transform);
    }
    private void CheckPlayerJump()
    {
        if (_isJumping)
        {
            return;
        }

        float triggeredJump = jumpAction.ReadValue<float>();
        if (triggeredJump != 1)
        {
            return;
        }

        animator.SetBool(runAnimationKey, false);
        currentRigidbody.linearVelocity = Vector2.up * jumpForce;
        HandleJumpAnimation();
    }
    private void HandleJumpAnimation()
    {
        _isJumping = true;

        float goingToX = currentRigidbody.transform.localScale.x;
        if (currentRigidbody.linearVelocityX > 0)
        {
            goingToX = 1;
        }
        else if (currentRigidbody.linearVelocityX < 0)
        {
            goingToX = -1;
        }

        currentRigidbody.transform.localScale = new Vector2(goingToX, 1);
        DOTween.Kill(currentRigidbody.transform);

        /*
         * Jumping animation
         * Removed animation from x scale to avoid auto turning player to the wrong side when changing the x value in the middle of the jumping animation
         */
        //currentRigidbody.transform.DOScaleX(scaleX * jumpScaleX, jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
        currentRigidbody.transform.DOScaleY(jumpScaleY, jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);

        StartCoroutine(AllowJumping());
    }

    IEnumerator AllowJumping()
    {
        // esperar segundos
        yield return new WaitForSeconds(jumpDurationInSeconds);
        _isJumping = false;
    }

    private void CheckPlayerMovement()
    {
        _isRunning = runAction.ReadValue<float>() == 1;
        _currentSpeed = _isRunning ? runSpeed : speed;
        animator.speed = _isRunning ? 2 : 1;

        float triggeredHorizontal = horizontalAction.ReadValue<float>();
        float localScaleX = currentRigidbody.transform.localScale.x;

        /* This will return 1 or -1 depending on the pressed key */
        if (triggeredHorizontal == 1 || triggeredHorizontal == -1)
        {
            currentRigidbody.linearVelocityX = triggeredHorizontal * _currentSpeed;

            /* Swipe to left or right */
            if (localScaleX != triggeredHorizontal)
            {
                currentRigidbody.transform.DOScaleX(triggeredHorizontal, playerSwipeDuration);
            }
            
            animator.SetBool(runAnimationKey, !_isJumping);
        }
        else
        {
            animator.SetBool(runAnimationKey, false);
        }

        HandleFriction();
    }
    private void HandleFriction() 
    {
        /* 
         * We removed friction from walls and floor so we add here to stop player little by little
         * IMPORTANT: We need to check the new velocity because if it is 0.8 and the current one is 0.5 it will not stop in 0, is going to be a mess
         */
        float currentVelocityX = currentRigidbody.linearVelocityX;

        if (currentVelocityX < 0)
        {
            float newVelocity = currentVelocityX + movementFriction;
            currentRigidbody.linearVelocityX = newVelocity < 0 ? newVelocity : 0;
        }
        else if (currentVelocityX > 0)
        {
            float newVelocity = currentVelocityX - movementFriction;
            currentRigidbody.linearVelocityX = newVelocity > 0 ? newVelocity : 0;
        }
    }

    private void PlayerOnKill()
    {
        /* Sempre remover para evitar ocupar espaço desnecessário na memória */
        healthBase.OnKill -= PlayerOnKill;
        animator.SetTrigger(triggerDeath);
    }
}
