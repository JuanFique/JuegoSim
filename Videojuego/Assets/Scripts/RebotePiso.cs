using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebotePiso : MonoBehaviour
{
    public Transform piso;             // Asigna el objeto del piso en el Inspector
    public float velocidadY = 0f;      // Velocidad vertical inicial
    public float gravedad = 0f;    // Gravedad hacia abajo
    public float energiaRebote = 0.8f; // Coeficiente de restituci�n (rebote)
    public float tolerancia = 0.01f;   // Peque�o margen para evitar pegado

    private bool enElPiso = false;

    void Update()
    {
        // Aplicar gravedad si no est� "pegado" al piso
        if (!enElPiso)
        {
            velocidadY += gravedad * Time.deltaTime;
        }

        // Mover el objeto manualmente
        transform.position += new Vector3(0, velocidadY * Time.deltaTime, 0);

        // Verificar colisi�n con el piso
        float yObjeto = transform.position.y;
        float yPiso = piso.position.y;

        if (yObjeto <= yPiso + tolerancia)
        {
            // Corregir la posici�n exacta al piso
            transform.position = new Vector3(transform.position.x, yPiso, transform.position.z);

            // Invertir la velocidad para rebotar con p�rdida de energ�a
            velocidadY = -velocidadY * energiaRebote;

            // Si la energ�a es muy baja, detener
            if (Mathf.Abs(velocidadY) < 0.1f)
            {
                velocidadY = 0f;
                enElPiso = true;
            }
        }
        else
        {
            enElPiso = false;
        }
    }
}
