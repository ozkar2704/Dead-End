using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script para controlar acciones del menú de opciones
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    /// <summary>
    /// Boton de volver
    /// </summary>
    public void Volver()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Botón de reset
    /// </summary>
    public void DeleteAll()
    {
        SQLiteDB.instance.ResetearTodo();
        SceneManager.LoadScene("OptionsMenu");
    }

    /// <summary>
    /// Botón de puntuaciones
    /// </summary>
    public void Puntuaciones()
    {
        SceneManager.LoadScene("PointsMenu");
    }

    public void Borrado()
    {
        SceneManager.LoadScene("DeleteScreen");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
}

