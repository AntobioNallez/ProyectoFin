using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed;
    public Vector3 direccionJugador;
    public Vector3 ultimaDireccion;
    public float vidaMaximaJugador;
    public float vidaJugador;

    public int experiencia;
    public int nivelActual;
    public int nivelMax;

    [SerializeField] private List<Weapon> armasInactivas;
    public List<Weapon> armasActivas;
    [SerializeField] private List<Weapon> armasMejorables;
    public List<Weapon> nivelMaximoArma;

    private bool esInmune;
    [SerializeField] private float inmunidadDuracion;
    [SerializeField] private float inmunacionTimer;

    public List<int> jugadorNiveles;

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
        ultimaDireccion = new Vector3(0, -1);
        for (int i = jugadorNiveles.Count; i < nivelMax; i++)
        {
            jugadorNiveles.Add(Mathf.CeilToInt(jugadorNiveles[jugadorNiveles.Count - 1] * 1.1f + 15));
        }
        vidaJugador = vidaMaximaJugador;
        UIController.Instance.ActualizarVida();
        UIController.Instance.ActualizarExp();
        AddWeapon(Random.Range(0, armasInactivas.Count));
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        direccionJugador = new Vector3(inputX, inputY).normalized;

        if (direccionJugador == Vector3.zero)
        {
            animator.SetBool("moving", false);
        }
        else if (Time.timeScale != 0)
        {
            animator.SetBool("moving", true);
            animator.SetFloat("moveX", inputX);
            animator.SetFloat("moveY", inputY);
            ultimaDireccion = direccionJugador;
        }

        if (inmunacionTimer > 0)
        {
            inmunacionTimer -= Time.deltaTime;
        }
        else
        {
            esInmune = false;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(direccionJugador.x * moveSpeed, direccionJugador.y * moveSpeed);
    }

    public void RecibirDamage(float damage)
    {
        if (!esInmune)
        {
            esInmune = true;
            inmunacionTimer = inmunidadDuracion;
            vidaJugador -= damage;
            UIController.Instance.ActualizarVida();
            if (vidaJugador <= 0)
            {
                gameObject.SetActive(false);
                GameManager.Instance.FinPartida();
            }
        }
    }

    public void ConseguirExp(int experienceToGet)
    {
        experiencia += experienceToGet;
        UIController.Instance.ActualizarExp();
        if (experiencia >= jugadorNiveles[nivelActual - 1])
        {
            SubirNivel();
        }
    }

    public void SubirNivel()
    {
        experiencia -= jugadorNiveles[nivelActual - 1];
        nivelActual++;
        UIController.Instance.ActualizarExp();
        //UIController.Instance.levelUpButtons[0].ActivateButton(activeWeapon);

        armasMejorables.Clear();

        if (armasActivas.Count > 0)
        {
            armasMejorables.AddRange(armasActivas);
        }
        if (armasInactivas.Count > 0)
        {
            armasMejorables.AddRange(armasInactivas);
        }
        for (int i = 0; i < UIController.Instance.botonesMejora.Length; i++)
        {
            if (armasMejorables.ElementAtOrDefault(i) != null)
            {
                UIController.Instance.botonesMejora[i].ActivateButton(armasMejorables[i]);
                UIController.Instance.botonesMejora[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.Instance.botonesMejora[i].gameObject.SetActive(false);
            }
        }

        UIController.Instance.AbrirPanelMejora();
    }

    private void AddWeapon(int index)
    {
        armasActivas.Add(armasInactivas[index]);
        armasInactivas[index].gameObject.SetActive(true);
        armasInactivas.RemoveAt(index);
    }

    public void ActivarWeapon(Weapon weapon)
    {
        weapon.gameObject.SetActive(true);
        armasActivas.Add(weapon);
        armasInactivas.Remove(weapon);
    }

    public void IncreaseMaxHealth(int value)
    {
        vidaMaximaJugador += value;
        vidaJugador = vidaMaximaJugador;
        UIController.Instance.ActualizarVida();

        UIController.Instance.CerrarPanelMejora();
        AudioController.Instance.ReproducirSonido(AudioController.Instance.seleccionMejora);
    }

    public void IncreaseMovementSpeed(float multiplier)
    {
        moveSpeed *= multiplier;

        UIController.Instance.CerrarPanelMejora();
        AudioController.Instance.ReproducirSonido(AudioController.Instance.seleccionMejora);
    }
}
