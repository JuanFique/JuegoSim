using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circulo : MonoBehaviour
{
    public float fuerzaInicial = 10f;
    public float gravedad = -9.81f;
    public float viento = 1f;
    public float energiaRebote = 0.8f; // 1 = rebote perfecto, <1 = pierde energía
    public float radio = 0.5f; // Radio para colisión manual
    private float left, right, top, bottom;

    // Hacemos pública la velocidad para que otros scripts puedan modificarla
    [HideInInspector]
    public Vector2 velocidad;

    private Vector2 aceleracion;

    public float alturaPiso = -3f; // altura Y del piso

    public void Inicializar(Vector2 direccionInicial, float fuerza)
    {
        velocidad = direccionInicial.normalized * fuerza;
    }

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

        // Verificar colisión con el piso (Y <= alturaPiso)
        if (nuevaPos.y <= alturaPiso)
        {
            nuevaPos.y = alturaPiso;

            // Rebote vertical con pérdida de energía
            velocidad.y = -velocidad.y * energiaRebote;

            // Detener si rebote es muy pequeño
            if (Mathf.Abs(velocidad.y) < 0.1f)
            {
                velocidad.y = 0f;
            }
        }

        // Aplicar la nueva posición
        transform.position = nuevaPos;
    }

    public void ReboteEnMadera()
    {
        // Rebote con factor 0.6 y reflejo vertical simple
        velocidad.y = -velocidad.y * 0.6f;

        // Reducir velocidad horizontal para simular fricción
        velocidad.x *= 0.8f;
    }
}
