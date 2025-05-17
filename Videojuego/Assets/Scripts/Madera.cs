using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madera : MonoBehaviour
{
    [Header("Hitbox Personalizada")]
    public float izquierda = -0.5f;
    public float abajo = -0.5f;
    public float ancho = 1f;
    public float alto = 1f;

    [Header("Propiedades Físicas")]
    public float densidad = 0.8f;
    public float elasticidad = 0.6f;
    public float amortiguacion = 0.2f;
    public float friccion = 0.9f;

    [Header("Configuración")]
    public float alturaPiso = -4.0f;
    public bool anclado = false;

    private Vector2 velocidad;
    private Vector2 fuerzaAcumulada;
    private Vector3 escalaOriginal;
    private Color colorOriginal;
    private float rotacionAcumulada;
    private bool enPiso = false;

    private float deformacionX = 0f;
    private float deformacionY = 0f;

    private float masa;
    private const float gravedad = -9.81f; // gravedad terrestre

    void Start()
    {
        escalaOriginal = transform.localScale;
        colorOriginal = GetComponent<SpriteRenderer>().color;
        masa = densidad * ancho * alto; // volumen 2D: ancho × alto
    }

    void Update()
    {
        if (anclado) return;

        AplicarFisicas();
        AplicarDeformacionVisual();
    }

    void AplicarFisicas()
    {
        // Aplicar gravedad proporcional a la masa
        fuerzaAcumulada += Vector2.up * gravedad * masa;

        // Aplicar fuerzas acumuladas
        velocidad += fuerzaAcumulada * Time.deltaTime;
        fuerzaAcumulada = Vector2.zero;

        Vector2 movimiento = velocidad * Time.deltaTime;
        transform.position += (Vector3)movimiento;

        // Rotación básica por movimiento horizontal
        if (movimiento.magnitude > 0.01f)
        {
            rotacionAcumulada += movimiento.x * 2f;
            transform.rotation = Quaternion.Euler(0, 0, rotacionAcumulada);
        }

        // Colisión con el piso
        if (transform.position.y - alto / 2f < alturaPiso)
        {
            transform.position = new Vector2(
                transform.position.x,
                alturaPiso + alto / 2f
            );

            velocidad.y = -velocidad.y * elasticidad * 0.5f;
            velocidad.x *= friccion;

            if (Mathf.Abs(velocidad.y) < 0.1f)
            {
                velocidad.y = 0;
                enPiso = true;
            }
        }

        // Amortiguación general
        velocidad *= (1f - amortiguacion * Time.deltaTime);
    }

    void AplicarDeformacionVisual()
    {
        float deformacionImpacto = Mathf.Clamp(fuerzaAcumulada.magnitude / 50f, -0.2f, 0.2f);

        deformacionX = Mathf.Lerp(deformacionX, deformacionImpacto, Time.deltaTime * 5f);
        deformacionY = Mathf.Lerp(deformacionY, -deformacionImpacto, Time.deltaTime * 5f);

        transform.localScale = new Vector3(
            escalaOriginal.x + deformacionX,
            escalaOriginal.y + deformacionY,
            escalaOriginal.z
        );

        float tension = Mathf.Clamp01(fuerzaAcumulada.magnitude / 100f);
        GetComponent<SpriteRenderer>().color = Color.Lerp(
            colorOriginal,
            Color.Lerp(Color.yellow, Color.red, tension),
            tension * 0.7f
        );
    }

    public void AplicarImpacto(Vector2 puntoImpacto, Vector2 fuerza)
    {
        if (anclado) return;

        Vector2 direccionImpacto = ((Vector2)transform.position - puntoImpacto).normalized;
        float magnitud = fuerza.magnitude;
        Vector2 fuerzaAplicada = direccionImpacto * magnitud * densidad;
        fuerzaAcumulada += fuerzaAplicada;

        CalcularDanioEstructural(magnitud, puntoImpacto, direccionImpacto);

        enPiso = false;
    }

    void CalcularDanioEstructural(float magnitudFuerza, Vector2 puntoImpacto, Vector2 direccionImpacto)
    {
        float distanciaAlCentro = Vector2.Distance(puntoImpacto, transform.position) / (ancho / 2f);
        float factorDebilidad = Mathf.Lerp(1.5f, 0.5f, distanciaAlCentro);
        // Este valor podría usarse luego para calcular rotura o grietas
    }

    public bool VerificarColisionPrecisa(Vector2 punto, float radio)
    {
        Rect hitbox = new Rect(
            transform.position.x + izquierda,
            transform.position.y + abajo,
            ancho,
            alto
        );

        Vector2 puntoMasCercano = new Vector2(
            Mathf.Clamp(punto.x, hitbox.xMin, hitbox.xMax),
            Mathf.Clamp(punto.y, hitbox.yMin, hitbox.yMax)
        );

        return Vector2.Distance(punto, puntoMasCercano) < radio;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Vector3 centro = new Vector3(
            transform.position.x + izquierda + ancho / 2f,
            transform.position.y + abajo + alto / 2f,
            0
        );
        Gizmos.DrawWireCube(centro, new Vector3(ancho, alto, 0));
    }
}
