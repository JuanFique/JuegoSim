using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Enemigos")]
    [SerializeField] private Transform contenedorDeEnemigos;

    [Header("Canvas")]
    [SerializeField] private GameObject endLevelWin;   // arrástrale "End level win"
    [SerializeField] private GameObject endLevelLose;  // arrástrale "End level lose"

    [Header("Control del cañón")]
    [SerializeField] private MonoBehaviour scriptCañon;

    private bool alreadyTriggered = false;

    void Start()
    {
        if (endLevelWin != null) endLevelWin.SetActive(false);
        if (endLevelLose != null) endLevelLose.SetActive(false);
    }

    void Update()
    {
        if (alreadyTriggered) return;

        if (TodosLosEnemigosMuertos())
        {
            ActivarVictoria();
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

    private void ActivarVictoria()
    {
        alreadyTriggered = true;

        if (scriptCañon != null) scriptCañon.enabled = false;
        if (endLevelWin != null) endLevelWin.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ActivarDerrota()
    {
        if (alreadyTriggered) return;

        alreadyTriggered = true;

        if (scriptCañon != null) scriptCañon.enabled = false;
        if (endLevelLose != null) endLevelLose.SetActive(true);

        Time.timeScale = 0f;
    }
}
