using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class hielo : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;

        // Masa del hielo: más ligera que la madera
        rb.mass = 0.5f;

        // Configuración del material físico para hacerlo resbaladizo
        PhysicsMaterial2D materialHielo = new PhysicsMaterial2D("MaterialHielo");
        materialHielo.friction = 0.05f; // Muy baja fricción
        materialHielo.bounciness = 0.1f; // Poca elasticidad

        Collider2D col = GetComponent<Collider2D>();
        col.sharedMaterial = materialHielo;
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
