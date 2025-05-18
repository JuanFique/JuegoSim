using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaderaMain : MonoBehaviour
{
    private Madera[] maderas;

    void Start()
    {
        // Obtener todos los bloques de madera hijos de este GameObject
        maderas = GetComponentsInChildren<Madera>();
    }

    public void ActualizarMaderas()
    {
        // Volver a obtener referencias por si algunas se destruyeron
        maderas = GetComponentsInChildren<Madera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
