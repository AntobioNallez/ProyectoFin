using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public AudioSource pausa;
    public AudioSource reanudar;
    public AudioSource enemigoMuerte;
    public AudioSource seleccionMejora;
    public AudioSource armaAreaSpawn;
    public AudioSource armaAreaDespawn;
    public AudioSource armaGiratoriaSpawn;
    public AudioSource armaGiratoriaDespawn;
    public AudioSource directionalWeaponSpawn;
    public AudioSource directionalWeaponHit;
    public AudioSource finPartida;

    private void Awake()
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

    public void ReproducirSonido(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }

    public void ReproducirSonidoModificado(AudioSource sound)
    {
        sound.pitch = Random.Range(0.7f, 1.3f);
        sound.Stop();
        sound.Play();
    }
}
