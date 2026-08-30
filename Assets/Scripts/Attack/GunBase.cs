using System.Collections;
using UnityEngine;

public class GunBase : Inputs
{
    public PoolManager poolManager;

    [Header("Shoot settings")]
    public Transform positionToStartShoot;
    public Transform sideRefference;
    public float timeBetweenShoot = .1f;
    private Coroutine _currentCoroutine;

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
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public void Shoot()
    {
        GameObject projectile = poolManager.GetPooledObject();
        if (projectile == null)
        {
            return;
        }

        projectile.transform.position = positionToStartShoot.position;

        ProjectileBase projectileBase = projectile.GetComponent<ProjectileBase>();
        if (projectileBase)
        {
            projectileBase.side = sideRefference.transform.localScale.x;
        }

        projectile.SetActive(true);
    }
}
