using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circulo : MonoBehaviour
{
    public float fuerzaInicial = 10f;
    public float gravedad = -9.81f;
    public float viento = 1f;
    public float energiaRebote = 0.8f; // 1 = rebote perfecto, <1 = pierde energ�a
    public float radio = 0.5f; // Radio para colisi�n manual
    private float left, right, top, bottom;

    // Hacemos p�blica la velocidad para que otros scripts puedan modificarla
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
        // Direcci�n hacia el mouse
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector2 direccion = (mouseWorldPos - transform.position).normalized;

        // Inicializar velocidad y aceleraci�n
        velocidad = direccion * fuerzaInicial;
        aceleracion = new Vector2(viento, gravedad);
    }

    void Update()
    {
        // Aplicar gravedad y viento
        velocidad += aceleracion * Time.deltaTime;

        // Mover objeto
        Vector2 nuevaPos = (Vector2)transform.position + velocidad * Time.deltaTime;

        // Verificar colisi�n con el piso (Y <= alturaPiso)
        if (nuevaPos.y <= alturaPiso)
        {
            nuevaPos.y = alturaPiso;

            // Rebote vertical con p�rdida de energ�a
            velocidad.y = -velocidad.y * energiaRebote;

            // Detener si rebote es muy peque�o
            if (Mathf.Abs(velocidad.y) < 0.1f)
            {
                velocidad.y = 0f;
            }
        }

        // Aplicar la nueva posici�n
        transform.position = nuevaPos;
    }

    public void ReboteEnMadera()
    {
        // Rebote con factor 0.6 y reflejo vertical simple
        velocidad.y = -velocidad.y * 0.6f;

        // Reducir velocidad horizontal para simular fricci�n
        velocidad.x *= 0.8f;
    }
}
