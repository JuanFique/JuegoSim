using System.Collections;
using UnityEngine;

public class EnemigosHandler : MonoBehaviour
{
    [SerializeField] private Transform contenedorDeEnemigos; // "Enemys"
    [SerializeField] private Canvas succesCanva;             // "End level"
    [SerializeField] private AudioClip levelPassed;
    [SerializeField] private MonoBehaviour scriptCañon;      // Script que controla el cañón

    private bool alreadyTriggered = false;
    private bool soundIsPlaying = false;

    void Start()
    {
        if (succesCanva != null)
            succesCanva.gameObject.SetActive(false);
    }

    void Update()
    {
        if (alreadyTriggered) return;

        if (TodosLosEnemigosMuertos())
        {
            alreadyTriggered = true;

            if (scriptCañon != null)
                scriptCañon.enabled = false; // 🔥 desactiva el cañón

            succesCanva.gameObject.SetActive(true);
            Time.timeScale = 0f;

        }
    }

    private bool TodosLosEnemigosMuertos()
    {
        foreach (Transform enemigo in contenedorDeEnemigos)
        {
            if (enemigo != null && enemigo.gameObject.activeSelf)
                return false;
        }
        return true;
    }
}
