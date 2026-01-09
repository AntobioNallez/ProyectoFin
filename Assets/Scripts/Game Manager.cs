using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameTime;
    public bool gameActive;

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

    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        if (gameActive)
        {
            gameTime += Time.deltaTime;
            UIController.Instance.UpdateTimer(gameTime);

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                Pausa();
            }
        }
    }

    public void FinPartida()
    {
        gameActive = false;
        StartCoroutine(MostrarFinPartida());
    }

    IEnumerator MostrarFinPartida()
    {
        yield return new WaitForSeconds(1.5f);
        UIController.Instance.panelFinJuego.SetActive(true);
        AudioController.Instance.ReproducirSonido(AudioController.Instance.finPartida);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Game");
    }

    public void Pausa()
    {
        if (UIController.Instance.panelMejora.activeSelf == false)
        {
            if (
                UIController.Instance.panelPausa.activeSelf == false &&
                UIController.Instance.panelFinJuego.activeSelf == false
                )
            {
                UIController.Instance.panelPausa.SetActive(true);
                Time.timeScale = 0f;
                AudioController.Instance.ReproducirSonido(AudioController.Instance.pausa);
            }
            else
            {
                UIController.Instance.panelPausa.SetActive(false);
                Time.timeScale = 1f;
                AudioController.Instance.ReproducirSonido(AudioController.Instance.reanudar);
            }
        }
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }

    public void IrAlMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }
}
