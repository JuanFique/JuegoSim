using UnityEngine;
using TMPro;

public class Crono : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameController gameController;

    [SerializeField] private float time = 180f;
    private bool failedLevel = false;

void Start()
{
    
    failedLevel = false;
}

void Awake()
{
    failedLevel = false;
    Debug.Log("Crono instanciado: " + gameObject.name);
}
 
    void Update()
    {
        if (failedLevel) return;

        time -= Time.deltaTime;
        if (time <= 0f)
        {
            time = 0f;
            failedLevel = true;

            if (gameController != null)
                gameController.ActivarDerrota();
        }

        int minutos = Mathf.FloorToInt(time / 60);
        int segundos = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutos:00}:{segundos:00}";
    }
}
