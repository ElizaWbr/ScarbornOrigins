using Core.Singleton;
using System.Collections.Generic;
using UnityEngine;
public class ProjectilePool : Singleton<ProjectilePool>
{
    /* Currently being used by GunBase */
    private GameObject _PFBProjectile;
    private PoolManager _poolManager;

    private PoolManager GetPoolManager()
    {
        if (Instance._poolManager == null)
        {
            Instance._poolManager = gameObject.GetComponent<PoolManager>();
        }
        return Instance._poolManager;
    }

    public static void SetProjectile(GameObject newProjectile)
    {
        if (newProjectile != Instance._PFBProjectile)
        {
            Instance._PFBProjectile = newProjectile;
            Instance.UpdateProjectile();
        }
    }

    public static GameObject GetPooledObject()
    {
        return Instance.GetPoolManager().GetPooledObject();
    }

    private void UpdateProjectile()
    {
        Instance.GetPoolManager().prefab = Instance._PFBProjectile;

        Instance.ClearList();
        Instance.RebuldList();
    }

    private void ClearList()
    {
        Instance.GetPoolManager().pooledObjects = new List<GameObject>();
    }

    private void RebuldList()
    {
        Instance.GetPoolManager().StartPool();
    }

}
