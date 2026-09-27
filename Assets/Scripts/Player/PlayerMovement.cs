using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class PlayerMovement : Inputs
{
    [Header("Player")]
    public SOPlayerSetup soPlayerSetup;
    public Rigidbody2D currentRigidbody;

    [Header("Health")]
    public HealthBase healthBase;

    //[Header(“Animation Setup”)]
    //public Animator animator;

    private float _currentSpeed;
    private bool _isRunning = false;
    private bool _isJumping = false;

    public GameObject currentPlayer;
    public GunBase currentBook;
    private Animator _currentAnimator;

    private void Awake()
    {
        LoadInputs();

        currentPlayer = Instantiate(soPlayerSetup.PFBPlayer, transform);

        if (healthBase != null)
        {
            healthBase.OnKill += PlayerOnKill;
            healthBase.flashColor = currentPlayer.GetComponentInChildren<FlashColor>();
        }

        _currentAnimator = currentPlayer.GetComponentInChildren<Animator>();

        GunSetup();
    }

    private void GunSetup()
    {
        /* We need to set Player values in GunBase script */

        GunBase currentGun = soPlayerSetup.SOAttackSetup.PFBBook;
        currentGun.soAttackSetup = soPlayerSetup.SOAttackSetup;
        currentGun.shooterRef = transform;
        currentBook = Instantiate(currentGun, transform);
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

        _currentAnimator.SetBool(soPlayerSetup.runAnimationKey, false);
        currentRigidbody.linearVelocity = Vector2.up * soPlayerSetup.jumpForce;
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
        currentRigidbody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.jumpEase);

        StartCoroutine(AllowJumping());
    }

    IEnumerator AllowJumping()
    {
        // esperar segundos
        yield return new WaitForSeconds(soPlayerSetup.jumpDurationInSeconds);
        _isJumping = false;
    }

    private void CheckPlayerMovement()
    {
        _isRunning = runAction.ReadValue<float>() == 1;
        _currentSpeed = _isRunning ? soPlayerSetup.runSpeed : soPlayerSetup.speed;
        _currentAnimator.speed = _isRunning ? 2 : 1;

        float triggeredHorizontal = horizontalAction.ReadValue<float>();
        float localScaleX = currentRigidbody.transform.localScale.x;

        /* This will return 1 or -1 depending on the pressed key */
        if (triggeredHorizontal == 1 || triggeredHorizontal == -1)
        {
            currentRigidbody.linearVelocityX = triggeredHorizontal * _currentSpeed;

            /* Swipe to left or right */
            if (localScaleX != triggeredHorizontal)
            {
                currentRigidbody.transform.DOScaleX(triggeredHorizontal, soPlayerSetup.playerSwipeDuration);
            }

            _currentAnimator.SetBool(soPlayerSetup.runAnimationKey, !_isJumping);
        }
        else
        {
            _currentAnimator.SetBool(soPlayerSetup.runAnimationKey, false);
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
            float newVelocity = currentVelocityX + soPlayerSetup.movementFriction;
            currentRigidbody.linearVelocityX = newVelocity < 0 ? newVelocity : 0;
        }
        else if (currentVelocityX > 0)
        {
            float newVelocity = currentVelocityX - soPlayerSetup.movementFriction;
            currentRigidbody.linearVelocityX = newVelocity > 0 ? newVelocity : 0;
        }
    }

    private void PlayerOnKill()
    {
        /* Sempre remover para evitar ocupar espaço desnecessário na memória */
        healthBase.OnKill -= PlayerOnKill;
        _currentAnimator.SetTrigger(soPlayerSetup.deathAnimationKey);
    }
}
