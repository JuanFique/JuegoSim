using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Piedra : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        rb.mass = 3f; // Más pesada que la madera
    }

    public void AplicarImpacto(Vector2 puntoImpacto, Vector2 fuerza)
    {
        rb.AddForceAtPosition(fuerza, puntoImpacto, ForceMode2D.Impulse);
    }

    public bool VerificarColision(Vector2 posicionCirculo, float radio)
    {
        return GetComponent<Collider2D>().OverlapPoint(posicionCirculo);
    }
}
