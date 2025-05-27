using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class EnemigoFisico : MonoBehaviour
{
    [Header("Vida y física")]
    public float vida = 100f;
    public float rebote = 0.4f;
    public float fuerzaMinimaParaDaño = 1.5f;
    public float tiempoVolverDePie = 0.5f;

    [Header("Sprites")]
    public Sprite spriteDePie;
    public Sprite spriteTumbado;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float tiempoUltimoGolpe = -999f;
    private GameObject ultimoObjetoGolpeado;
    private float cooldownEntreGolpes = 0.3f;
    private bool fueGolpeadoEsteFrame = false;
    private bool yaEstaTumbado = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sr.sprite = spriteDePie;

        rb.gravityScale = 1f;
        rb.mass = 5f;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if (!fueGolpeadoEsteFrame && Time.time - tiempoUltimoGolpe >= tiempoVolverDePie)
        {
            sr.sprite = spriteDePie;
            yaEstaTumbado = false;
        }

        fueGolpeadoEsteFrame = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider || collision.contactCount == 0) return;

        Vector2 fuerza = collision.relativeVelocity;
        float magnitud = fuerza.magnitude;

        if (magnitud >= fuerzaMinimaParaDaño)
        {
            if (collision.gameObject.TryGetComponent(out Circulo bala))
            {
                vida *= 0.5f;
            }
            else
            {
                if (collision.gameObject != ultimoObjetoGolpeado || Time.time - tiempoUltimoGolpe > cooldownEntreGolpes)
                {
                    vida -= 10f;
                    ultimoObjetoGolpeado = collision.gameObject;
                    tiempoUltimoGolpe = Time.time;
                }
            }

            Vector2 normal = collision.GetContact(0).normal;
            rb.velocity = Vector2.Reflect(rb.velocity, normal) * rebote;

            sr.sprite = spriteTumbado;
            fueGolpeadoEsteFrame = true;
            yaEstaTumbado = true;
        }
    }

    public void RecibirGolpeDesdeBala(Vector2 fuerzaImpacto)
    {
        if (vida <= 0) return;

        vida -=50f;

        sr.sprite = spriteTumbado;
        fueGolpeadoEsteFrame = true;
        tiempoUltimoGolpe = Time.time;
        yaEstaTumbado = true;

        rb.velocity += fuerzaImpacto * rebote;
    }
}
