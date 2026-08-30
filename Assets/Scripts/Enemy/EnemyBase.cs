using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;

    [Header("Animator Setup")]
    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerDeath = "Death";

    [Header("Health Settings")]
    public HealthBase healthBase;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnEnemyKill;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthBase healthBase = collision.gameObject.GetComponent<HealthBase>();
        if (healthBase != null)
        {
            healthBase.Damage(damage);
            PlayAttackAnimation();
        }
    }

    private void OnEnemyKill()
    { 
        /* Remover o callback da memória para nãop armazenar dados desnecessários */
        healthBase.OnKill -= OnEnemyKill;
        PlayDeathAnimation();
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    private void PlayDeathAnimation()
    {
        animator.SetTrigger(triggerDeath);
    }
}
