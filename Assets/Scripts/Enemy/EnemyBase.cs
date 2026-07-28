using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthBase healthBase = collision.gameObject.GetComponent<HealthBase>();
        if (healthBase != null)
        {
            healthBase.Damage(damage);
        }
    }
}
