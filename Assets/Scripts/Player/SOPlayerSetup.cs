using DG.Tweening;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    public GameObject PFBPlayer;

    [Header("Book")]
    public SOAttackSetup SOAttackSetup;

    //public Vector3 bookPosition;

    [Header("Player movement")]
    public float speed = 15;
    public float runSpeed = 30;
    public float jumpForce = 23;
    public float jumpDurationInSeconds = 1f;
    public float movementFriction = .8f;

    [Header("Animation Setup")]
    public string runAnimationKey = "Run";
    public string deathAnimationKey = "Death";//triggerDeath
    public float playerSwipeDuration = .1f;
    public float jumpScaleY = 1.2f;
    public float jumpScaleX = 0.8f;
    public float jumpAnimationDuration = .3f;
    public Ease jumpEase = Ease.OutBack;
}
