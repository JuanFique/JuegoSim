using UnityEngine;
using TMPro;

public class Crono : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameController gameController; // arrástrale el GameController

    [SerializeField] private float time = 180f;
    private bool failedLevel = false;

void Update()
{
    if (failedLevel) return;

    time -= Time.deltaTime;

    if (time <= 0f)
    {
        time = 0f; // 🔒 Fijar el tiempo en cero visualmente
        failedLevel = true;

        if (gameController != null)
            gameController.ActivarDerrota();
    }

    int minutos = Mathf.FloorToInt(time / 60);
    int segundos = Mathf.FloorToInt(time % 60);
    timerText.text = $"{minutos:00}:{segundos:00}";
}

}
