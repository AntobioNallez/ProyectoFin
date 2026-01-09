using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private TMP_Text vidaTexto;
    [SerializeField] private Slider playerExperienceSlider;
    [SerializeField] private TMP_Text expTexto;
    public GameObject panelFinJuego;
    public GameObject panelPausa;
    public GameObject panelMejora;
    [SerializeField] private TMP_Text timerText;

    public LevelUpButton[] botonesMejora;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ActualizarVida()
    {
        playerHealthSlider.maxValue = PlayerController.Instance.vidaMaximaJugador;
        playerHealthSlider.value = PlayerController.Instance.vidaJugador;
        vidaTexto.text = playerHealthSlider.value + " / " + playerHealthSlider.maxValue;
    }

    public void ActualizarExp()
    {
        playerExperienceSlider.maxValue = PlayerController.Instance.jugadorNiveles[PlayerController.Instance.nivelActual - 1];
        playerExperienceSlider.value = PlayerController.Instance.experiencia;
        expTexto.text = playerExperienceSlider.value + " / " + playerExperienceSlider.maxValue;
    }

    public void UpdateTimer(float timer)
    {
        float min = Mathf.FloorToInt(timer / 60f);
        float sec = Mathf.FloorToInt(timer % 60f);

        timerText.text = min + ":" + sec.ToString("00");
    }

    public void AbrirPanelMejora()
    {
        panelMejora.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CerrarPanelMejora()
    {
        panelMejora.SetActive(false);
        Time.timeScale = 1f;
    }
}
