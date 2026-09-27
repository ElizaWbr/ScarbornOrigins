using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public SOAttackSetup soAttackSetup;
    public float side = 1;
    private float _currentSide;
    private void OnEnable()
    {
        /* Executa quando SetActive(true) é chamado */
        _currentSide = side;
        StartCoroutine(TimerToDisableItem());
    }

    IEnumerator TimerToDisableItem()
    {
        /* Espera alguns segundos para executar, o fluxo continua depois do return */
        yield return new WaitForSeconds(soAttackSetup.timeToDestroy);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(soAttackSetup.direction * _currentSide * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.transform.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            HealthBase healthBase = collision.transform.GetComponent<HealthBase>();
            healthBase.Damage(soAttackSetup.damageAmount);
            gameObject.SetActive(false);
        }
    }
}
