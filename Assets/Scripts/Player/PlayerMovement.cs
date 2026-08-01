using DG.Tweening;
using UnityEngine;

public class PlayerMovement : PlayerInputs
{

    [Header("Player")]
    public Rigidbody2D currentRigidbody;

    [Header("Player movement")]
    public float speed = 15;
    public float runSpeed = 30;
    public float jumpForce = 15;
    public float movementFriction = .8f;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.2f;
    public float jumpScaleX = 0.8f;
    public float jumpAnimationDuration = .3f;
    public Ease jumpEase = Ease.OutBack;

    public float playerSwipeDuration = .1f;

    private float _currentSpeed;
    private bool _isRunning;

    private void Update()
    {
        CheckPlayerJump();
        CheckPlayerMovement();
    }
    private void OnDestroy()
    {
        DOTween.Kill(currentRigidbody.transform);
    }
    private void CheckPlayerJump()
    {
        float triggeredJump = jumpAction.ReadValue<float>();
        if (triggeredJump != 1)
        {
            return;
        }

        currentRigidbody.linearVelocity = Vector2.up * jumpForce;
        HandleJumpAnimation();
    }
    private void HandleJumpAnimation()
    {
        currentRigidbody.transform.localScale = Vector2.one;
        DOTween.Kill(currentRigidbody.transform);

        currentRigidbody.transform.DOScaleY(jumpScaleY, jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
        currentRigidbody.transform.DOScaleX(jumpScaleX, jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase);
    }
    private void CheckPlayerMovement()
    {
        _isRunning = runAction.ReadValue<float>() == 1;
        _currentSpeed = _isRunning ? runSpeed : speed;

        /* This will return 1 or -1 depending on the pressed key */
        float triggeredHorizontal = horizontalAction.ReadValue<float>();
        if (triggeredHorizontal == 1 || triggeredHorizontal == -1)
        {
            currentRigidbody.linearVelocityX = triggeredHorizontal * _currentSpeed;

            /* Swipe to left or right */
            if (currentRigidbody.transform.localScale.x != triggeredHorizontal)
            {
                currentRigidbody.transform.DOScaleX(triggeredHorizontal, playerSwipeDuration);
            }
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
}
