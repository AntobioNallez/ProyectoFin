using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void NuevaPartida()
    {
        SceneManager.LoadScene("Game");
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
