using DG.Tweening;
using System;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;

    public FlashColor flashColor;

    [Header("Heath base")]
    public int startLife;
    public bool destroyOnDeath;
    public float delayToDestroy = .5f;

    private int _currentLife;
    private bool _isDead;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _currentLife = startLife;
        _isDead = false;
    }

    public void Damage(int damage)
    {
        if (_isDead)
        {
            return;
        }
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            Death();
        }

        if (flashColor != null)
        {
            flashColor.Flash();
        }
    }

    private void Death()
    {
        _isDead = true;

        /* Apenas executa se OnKill for diferente de nulo */
        OnKill?.Invoke();

        if (destroyOnDeath)
        {
            Destroy(gameObject, delayToDestroy);
        }

        
    }

    public bool isDead()
    {
        return _isDead;
    }
}
