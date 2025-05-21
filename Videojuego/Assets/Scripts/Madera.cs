using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Madera : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        rb.mass = 1f; // MÁS liviana que la piedra
    }

    public void AplicarImpacto(Vector2 puntoImpacto, Vector2 fuerza)
    {
        rb.AddForceAtPosition(fuerza, puntoImpacto, ForceMode2D.Impulse);
    }

    public bool VerificarColisionPrecisa(Vector2 posicionCirculo, float radio)
    {
        return GetComponent<Collider2D>().OverlapPoint(posicionCirculo);
    }
}
