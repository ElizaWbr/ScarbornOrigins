using System.Collections;
using UnityEngine;

public class GunBase : Inputs
{
    [Header("Shoot settings")]
    public SOAttackSetup soAttackSetup;
    public Transform shooterRef;
    private Coroutine _currentCoroutine;
    protected override void InputsAwake()
    {
        soAttackSetup.projectilePrefab.GetComponent<ProjectileBase>().soAttackSetup = soAttackSetup;
        ProjectilePool.SetProjectile(soAttackSetup.projectilePrefab);
    }
    private void Update()
    {
        if (shootAction.IsPressed())
        {
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(StartShoot());
            }
        }
        else if (shootAction.WasReleasedThisDynamicUpdate())
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(soAttackSetup.timeBetweenShoot);
        }
    }

    public void Shoot()
    {
        GameObject projectile = ProjectilePool.GetPooledObject();
        if (projectile == null)
        {
            return;
        }

        projectile.transform.position = transform.position;

        ProjectileBase projectileBase = projectile.GetComponent<ProjectileBase>();
        if (projectileBase)
        {
            projectileBase.side = shooterRef.transform.localScale.x;
        }

        projectile.SetActive(true);
    }
}
