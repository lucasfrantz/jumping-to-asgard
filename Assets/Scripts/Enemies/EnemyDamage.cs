using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<Health>().TakeDamage(damage);
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            float x = rb.velocity.x;
            float y = rb.velocity.y;
            rb.velocity = new Vector2(-x/2, -y/2);
    }
}
