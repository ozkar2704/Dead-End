using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Script para definir las acciones de la pantalla de puntuaciones
/// </summary>
public class PointMenu : MonoBehaviour
{
    private List<int> partidas;

    /// <summary>
    /// Recogemos y mostramos las puntuaciones de las partidas finalizadas
    /// </summary>
    private void Awake()
    {
        partidas = SQLiteDB.instance.SelectPartidasFinalizadas();
        if (partidas != null && partidas.Count > 0)
        {
            partidas.Sort();
            partidas.Reverse();
            int contador = 0;
            foreach (int p in partidas)
            {
                if (contador < 6)
                {
                    GameObject.Find("Text" + contador).GetComponentInChildren<UnityEngine.UI.Text>().text = p.ToString() + " / 100";
                    contador++;
                } else
                {
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Botón de volver
    /// </summary>
    public void Volver()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
