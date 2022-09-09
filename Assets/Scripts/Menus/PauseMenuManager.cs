using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script para controlar el menú de pausa
/// </summary>
public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager instance;

    [Header("Pause menu")]
    [SerializeField] private GameObject PauseMenu;

    public bool isPauseMenuActive = false;

    /// <summary>
    /// Al iniciar la clase escondemos el menú
    /// </summary>
    private void Awake()
    {
        if (PauseMenuManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        PauseMenu.SetActive(isPauseMenuActive);
    }

    /// <summary>
    /// Mostrar el menú si se pulsa la tecla de menú(m)
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isPauseMenuActive)
            {
                PauseMenu.SetActive(false);
                isPauseMenuActive = false;
            }
            else
            {
                PauseMenu.SetActive(true);
                isPauseMenuActive = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && isPauseMenuActive)
        {
            Continuar();
        }

        if (Input.GetKeyDown(KeyCode.Z) && isPauseMenuActive)
        {
            MenuPrincipal();
        }

        if (Input.GetKeyDown(KeyCode.X) && isPauseMenuActive)
        {
            BorrarPartida();
        }
    }

    /// <summary>
    /// Botón borrar partida
    /// </summary>
    public void BorrarPartida()
    {
        PauseMenu.SetActive(false);
        isPauseMenuActive = false;
        GameManager.instance.BorrarPartida();
    }

    /// <summary>
    /// Boton de volver al menú principal
    /// </summary>
    public void MenuPrincipal()
    {
        PauseMenu.SetActive(false);
        isPauseMenuActive = false;
        GameManager.instance.SaveState();
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Botón de contiuar
    /// </summary>
    public void Continuar()
    {
        PauseMenu.SetActive(false);
        isPauseMenuActive = false;
    }
}
