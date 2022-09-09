using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script para controlar acciones del menú principal
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Acciones al pulsar el boton play
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene("PlayMenu");
    }

    /// <summary>
    /// Acciones al pulsar el boton Opciones
    /// </summary>
    public void Options()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    /// <summary>
    /// Cierra la aplicación
    /// </summary>
    public void Salir()
    {
        Application.Quit();
    }
}
