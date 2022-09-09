using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Script para definir las acciones del ménu de juego
/// </summary>
public class PlayMenu : MonoBehaviour
{
    private List<string> partidas;
    private List<int> partidasOriginal;

    /// <summary>
    /// Recogemos y mostramos las partidas activas
    /// </summary>
    private void Awake()
    {
        partidasOriginal = SQLiteDB.instance.SelectPartidas();
        partidas = new List<string>();
        if (partidasOriginal != null && partidasOriginal.Count > 0)
        {
            Partida partida;
            
            foreach(int p in partidasOriginal)
            {
                partida = SQLiteDB.instance.GetPartidaById(p);
                if (partida != null)
                {
                    partidas.Add(partida.Nombre);
                }
            }
            if (partidas.Count > 0)
            {
                int contador = 1;
                foreach (string p in partidas)
                {
                    GameObject.Find("Boton" + contador).GetComponentInChildren<UnityEngine.UI.Text>().text = p;
                    contador++;
                }
            }
        } else
        {
            SQLiteDB.instance.ResetearTodo();
        }
    }

    /// <summary>
    /// Al pulsar una partida, si está activa la iniciamos y si no creamos una
    /// </summary>
    /// <param name="buton"></param>
    public void Play(Button buton)
    {
        string p;
        PlayerPrefs.DeleteAll();
        if (!buton.GetComponentInChildren<UnityEngine.UI.Text>().text.Equals("Nueva Partida")) {
            p = buton.GetComponentInChildren<UnityEngine.UI.Text>().text;
        } else
        {
            p = "Partida " + (partidasOriginal.Count + 1);
            SQLiteDB.instance.InsertPartida(p, "Intro", 0, 0);
        }

        Partida partida = SQLiteDB.instance.GetPartida(p);
        if (partida != null)
        {
            PlayerPrefs.SetInt("partidaId", partida.Id);
            PlayerPrefs.SetString("escena", partida.Escena);
        }

        SceneManager.LoadScene(partida.Escena);
    }

    /// <summary>
    /// Botón de volver
    /// </summary>
    public void Volver()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
