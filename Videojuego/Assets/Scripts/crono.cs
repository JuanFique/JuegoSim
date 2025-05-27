using UnityEngine;
using TMPro;

public class Crono : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Canvas failCanva;
    [SerializeField] private float time = 180f; // 3 minutos por defecto

    private bool failedLevel = false;
    public bool FailedLevel { get; set; }

    private int timerMinutes, timerSeconds;

    void Start()
    {
        failCanva.gameObject.SetActive(false);
    }

    void Update()
    {
        if (FailedLevel) return;

        time -= Time.deltaTime;

        timerMinutes = Mathf.FloorToInt(time / 60);
        timerSeconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", timerMinutes, timerSeconds);

        if (time <= 0 && !FailedLevel)
        {
            FailedLevel = true;
            Time.timeScale = 0f;
            failCanva.gameObject.SetActive(true);
            Debug.Log("⏱ Tiempo agotado - Nivel fallido");
        }
    }
}
