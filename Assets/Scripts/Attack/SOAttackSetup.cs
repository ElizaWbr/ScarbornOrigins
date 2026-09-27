using UnityEngine;

[CreateAssetMenu]
public class SOAttackSetup : ScriptableObject
{
    [Header("Gun")]
    public GunBase PFBBook;
    public float timeBetweenShoot = .1f;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Vector3 direction = new Vector3(15, 0, 0);
    public float timeToDestroy = 2f;
    public int damageAmount = 1;

}
