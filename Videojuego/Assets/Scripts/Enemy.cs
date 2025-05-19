using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Rigidbody2D rb;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        // Al inicio desactivamos las físicas (si deseas que esté quieto hasta ser golpeado)
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (IsBall(other))
        {
            // Activar físicas para que se vea afectado por el impacto
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f;

            // Aplicar una fuerza con base en la velocidad relativa del impacto
            Vector2 impactForce = collision.relativeVelocity * 10f;
            rb.AddForce(impactForce, ForceMode2D.Impulse);

            // Destruir la bala
            Destroy(other);

            // Mensaje y eliminación
            Debug.Log($"{gameObject.name} murió");
            Destroy(gameObject);
        }
        else
        {
            TakeDamage(maxHealth * 0.3f);
        }
    }

    private bool IsBall(GameObject obj)
    {
        return obj.GetComponent<Circulo>() != null;
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Debug.Log($"{gameObject.name} murió");
            Destroy(gameObject);
        }
    }
}
