using UnityEditor;
using UnityEngine;

public class GravedadLuna : MonoBehaviour
{
    public float gravedadLunar = -1.62f; // Gravedad lunar en m/s²

    void Start()
    {
        AplicarGravedadALosObjetos<Madera>();
        AplicarGravedadALosObjetos<Piedra>();
        AplicarGravedadALosObjetos<hielo>();
    }

    void AplicarGravedadALosObjetos<T>() where T : MonoBehaviour
    {
        T[] objetos = FindObjectsOfType<T>();

        foreach (T obj in objetos)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Desactiva la gravedad de Unity
                rb.gravityScale = 0f;

                // Aplica gravedad lunar personalizada como fuerza constante
                rb.AddForce(Vector2.down * Mathf.Abs(gravedadLunar) * rb.mass, ForceMode2D.Force);
            }
        }
    }
}
