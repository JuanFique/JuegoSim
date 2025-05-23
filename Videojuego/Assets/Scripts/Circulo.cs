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
    public int pasosPorFrame = 4;

    [HideInInspector] public Vector2 velocidad;
    private Vector2 aceleracion;

    private bool impactoDetectado = false;
    public bool esInstanciado = false; 

    void Start()
    {
        if (esInstanciado)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            Vector2 direccion = (mouseWorldPos - transform.position).normalized;

            velocidad = direccion * fuerzaInicial;
            aceleracion = new Vector2(viento, gravedad);
        }
    }

    void Update()
    {
        if (velocidad == Vector2.zero) return;

        float dt = Time.deltaTime / pasosPorFrame;

        for (int i = 0; i < pasosPorFrame; i++)
        {
            velocidad += aceleracion * dt;
            Vector2 nuevaPos = (Vector2)transform.position + velocidad * dt;

            // COLISIÓN CON EL PISO
            if (nuevaPos.y - radio <= alturaPiso)
            {
                nuevaPos.y = alturaPiso + radio;
                if (Mathf.Abs(velocidad.y) > 0.05f)
                    velocidad.y = -velocidad.y * energiaRebote;
                else
                    velocidad.y = 0;

                velocidad.x *= friccionPiso;

                if (esInstanciado && !impactoDetectado) RegistrarImpacto();
            }

            // COLISIÓN CON MADERAS
            Madera[] todasMaderas = FindObjectsOfType<Madera>();
            foreach (Madera madera in todasMaderas)
            {
                if (madera.VerificarColisionPrecisa(nuevaPos, radio))
                {
                    Vector2 puntoImpacto = nuevaPos;
                    Vector2 fuerzaImpacto = velocidad * radio * 0.5f;

                    madera.AplicarImpacto(puntoImpacto, fuerzaImpacto);

                    Vector2 normal = ((Vector2)transform.position - puntoImpacto).normalized;
                    velocidad = Vector2.Reflect(velocidad, normal) * 0.7f;
                    nuevaPos += normal * (radio * 1.05f);

                    if (esInstanciado && !impactoDetectado) RegistrarImpacto();
                    break;
                }
            }

            // COLISIÓN CON PIEDRAS
            Piedra[] todasPiedras = FindObjectsOfType<Piedra>();
            foreach (Piedra piedra in todasPiedras)
            {
                if (piedra.VerificarColision(nuevaPos, radio))
                {
                    Vector2 puntoImpacto = nuevaPos;
                    Vector2 fuerzaImpacto = velocidad * radio * 0.5f;

                    piedra.AplicarImpacto(puntoImpacto, fuerzaImpacto);

                    Vector2 normal = ((Vector2)transform.position - puntoImpacto).normalized;
                    velocidad = Vector2.Reflect(velocidad, normal) * 0.7f;
                    nuevaPos += normal * (radio * 1.05f);

                    if (esInstanciado && !impactoDetectado) RegistrarImpacto();
                    break;
                }
            }

            transform.position = nuevaPos;

            if (velocidad.magnitude < 0.05f)
                velocidad = Vector2.zero;
        }
    }

    public void Inicializar(Vector2 direccionInicial, float fuerza)
    {
        velocidad = direccionInicial.normalized * fuerza;
        aceleracion = new Vector2(viento, gravedad);
        esInstanciado = true;
    }

    private void RegistrarImpacto()
    {
        impactoDetectado = true;
        Invoke("Destruir", 5f);
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }
}
