using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circulo : MonoBehaviour
{
    public float fuerzaInicial = 10f;
    public float gravedad = -9.81f;
    public float viento = 1f;
    public float energiaRebote = 0.8f;
    public float radio = 0.5f;
    public float alturaPiso = -2.0f;
    public float friccionPiso = 0.9f;

    [HideInInspector] public Vector2 velocidad;
    private Vector2 aceleracion;

    void Start()
    {
        // Dirección hacia el mouse
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector2 direccion = (mouseWorldPos - transform.position).normalized;

        // Inicializar velocidad y aceleración
        velocidad = direccion * fuerzaInicial;
        aceleracion = new Vector2(viento, gravedad);
    }

    void Update()
    {
        // Aplicar gravedad y viento
        velocidad += aceleracion * Time.deltaTime;

        // Mover objeto
        Vector2 nuevaPos = (Vector2)transform.position + velocidad * Time.deltaTime;

        // Verificar colisión con el piso
        if (nuevaPos.y - radio <= alturaPiso)
        {
            nuevaPos.y = alturaPiso + radio;
            velocidad.y = -velocidad.y * energiaRebote;
            velocidad.x *= friccionPiso;

            if (Mathf.Abs(velocidad.y) < 0.1f)
            {
                velocidad.y = 0f;
            }
        }

        // Verificar colisión con maderas
        Madera[] todasMaderas = FindObjectsOfType<Madera>();
        foreach (Madera madera in todasMaderas)
        {
            if (madera.VerificarColisionPrecisa(nuevaPos, radio))
            {
                Vector2 puntoImpacto = nuevaPos;
                Vector2 fuerzaImpacto = velocidad * radio * 0.5f; // Factor de masa

                // Aplica impacto a la madera
                madera.AplicarImpacto(puntoImpacto, fuerzaImpacto);

                // Rebote del proyectil
                Vector2 normal = ((Vector2)transform.position - puntoImpacto).normalized;
                velocidad = Vector2.Reflect(velocidad, normal) * 0.7f;

                break; // Solo un impacto por frame
            }
        }

        // Aplicar movimiento
        transform.position = nuevaPos;

        // Detener si la velocidad es muy baja
        if (velocidad.magnitude < 0.1f)
        {
            velocidad = Vector2.zero;
        }

        Piedra[] todasPiedras = FindObjectsOfType<Piedra>();
        foreach (Piedra piedra in todasPiedras)
        {
            if (piedra.VerificarColision(nuevaPos, radio))
            {
                Vector2 puntoImpacto = nuevaPos;
                Vector2 fuerzaImpacto = velocidad * radio * 0.5f;

                // Aplica fuerza a la piedra
                piedra.AplicarImpacto(puntoImpacto, fuerzaImpacto);

                // Rebote del proyectil
                Vector2 normal = ((Vector2)transform.position - puntoImpacto).normalized;
                velocidad = Vector2.Reflect(velocidad, normal) * 0.7f;

                break;
            }
        }
    }

    public void Inicializar(Vector2 direccionInicial, float fuerza)
    {
        velocidad = direccionInicial.normalized * fuerza;
    }
}