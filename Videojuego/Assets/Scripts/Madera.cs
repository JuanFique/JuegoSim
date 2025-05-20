using UnityEngine;

public class Madera : MonoBehaviour
{
    [Header("Tamaño visual para colisiones")]
    public float izquierda = -0.5f;
    public float abajo = -0.5f;
    public float ancho = 1f;
    public float alto = 1f;

    public Vector2 velocidad;
    public float masa = 1f;
    public float friccion = 0.98f;
    public float energiaRebote = 0.6f;
    public float alturaPiso = -2.0f;

    private Vector2 aceleracion = Vector2.zero;

    public Rect ObtenerRect()
    {
        return new Rect(
            transform.position.x + izquierda,
            transform.position.y + abajo,
            ancho,
            alto
        );
    }

    public bool VerificarColisionPrecisa(Vector2 centro, float radio)
    {
        Rect hitbox = ObtenerRect();
        Vector2 puntoCercano = new Vector2(
            Mathf.Clamp(centro.x, hitbox.xMin, hitbox.xMax),
            Mathf.Clamp(centro.y, hitbox.yMin, hitbox.yMax)
        );
        float distancia = Vector2.Distance(centro, puntoCercano);
        return distancia <= radio;
    }

    public void AplicarImpacto(Vector2 puntoImpacto, Vector2 fuerzaImpacto)
    {
        Vector2 impulso = fuerzaImpacto / masa;
        velocidad += impulso;
    }
    void Update()
    {
        // Movimiento si tiene velocidad
        if (velocidad.magnitude > 0.01f)
        {
            Vector2 nuevaPos = (Vector2)transform.position + velocidad * Time.deltaTime;

            // Colisión con el piso
            if (nuevaPos.y <= alturaPiso + alto / 2f)
            {
                nuevaPos.y = alturaPiso + alto / 2f;
                velocidad.y = -velocidad.y * energiaRebote;
                velocidad.x *= friccion;
            }

            transform.position = nuevaPos;
            velocidad *= friccion;
        }
        else
        {
            velocidad = Vector2.zero;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 centro = new Vector3(
            transform.position.x + izquierda + ancho / 2f,
            transform.position.y + abajo + alto / 2f,
            0
        );
        Gizmos.DrawWireCube(centro, new Vector3(ancho, alto, 0));
    }
}