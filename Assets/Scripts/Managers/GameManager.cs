using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador general del juego
/// Controla las acciones generales del juego y se ocupa de manejar y guardar los datos necesarios
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public FloatingTextManager FloatingTextManager;

    //Juego
    public List<string> keys;
    public List<int> values;
    public List<string> noConoceIndex;
    public List<int> noConoceValues;
    public string Nivel;
    public int NivelId;
    public Partida partida;

    //Mostrar un texto usando la clase FloatingText
    public FloatingText ShowText(string msg, int fontsize, Color color, Vector3 position, string lookAt, Vector3 motion, float duration)
    {
        return FloatingTextManager.Show(msg, fontsize, color, position, lookAt, motion, duration);
    }

    /// <summary>
    /// Inicializa la instancia y los datos necesarios
    /// Obtenemos los datos necesarios de la base de datos
    /// </summary>
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        keys = null;
        keys = SQLiteDB.instance.SelectInventary(0);
        values = new List<int>();
        for (int i = 0; i < keys.Count; i++)
        {
            values.Add(0);
        }
        noConoceIndex = null;
        noConoceIndex = SQLiteDB.instance.SelectInventary(1);
        noConoceValues = new List<int>();
        for (int i = 0; i < noConoceIndex.Count; i++)
        {
            noConoceValues.Add(0);
        }
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
        //Utilizamos las PlayerPrefs para obtener el id de la partida desde el menú de juego
        partida = SQLiteDB.instance.GetPartidaById(PlayerPrefs.GetInt("partidaId"));
    }

    /// <summary>
    /// Al destruir el objeto guardamos la partida
    /// </summary>
    private void OnDestroy()
    {
        SaveState();
    }

    /// <summary>
    /// Método para guardar los datos de la partida
    /// Se guardarán unos datos u otros en base al punto en el que nos encontremos
    /// </summary>
    public void SaveState()
    {

        partida = SQLiteDB.instance.GetPartidaById(PlayerPrefs.GetInt("partidaId"));
        if (partida != null)
        {
            NivelId = SQLiteDB.instance.GetNivelId(Nivel, partida.Id);

            if (SceneManager.GetActiveScene().name.Contains("DeadEnd"))
            {
                SQLiteDB.instance.UpdatePartida(partida.Id, "Start", partida.Puntos, partida.DeadEnds + 1);
                SQLiteDB.instance.DeleteItems(NivelId);//En teoría no haría falta borrar en estas tres tablas por el delete on cascade pero no funciona
                SQLiteDB.instance.DeleteConoce(NivelId);
                SQLiteDB.instance.DeleteCollect(partida.Id);
                SQLiteDB.instance.DeleteNivel(Nivel, partida.Id);
            }
            else if (SceneManager.GetActiveScene().name.Contains("End"))
            {
                partida.Puntos = Calcular();
                SQLiteDB.instance.UpdatePartida(partida.Id, SceneManager.GetActiveScene().name, partida.Puntos, partida.DeadEnds);
                SQLiteDB.instance.DeleteItems(NivelId);//En teoría no haría falta borrar en estas tres tablas por el delete on cascade pero no funciona
                SQLiteDB.instance.DeleteConoce(NivelId);
                SQLiteDB.instance.DeleteCollect(partida.Id);
                SQLiteDB.instance.DeleteNivel(Nivel, partida.Id);
            }
            else
            {
                SQLiteDB.instance.UpdatePartida(partida.Id, SceneManager.GetActiveScene().name, partida.Puntos, partida.DeadEnds);

                SQLiteDB.instance.DeleteItems(NivelId);//En teoría no haría falta borrar en estas dos tablas por el delete on cascade pero no funciona
                SQLiteDB.instance.DeleteConoce(NivelId);
                SQLiteDB.instance.DeleteNivel(Nivel, partida.Id);
                SQLiteDB.instance.InsertNivel(partida.Id, Nivel);

                NivelId = SQLiteDB.instance.GetNivelId(Nivel, partida.Id);

                foreach (string item in GameManager.instance.keys)
                {
                    SQLiteDB.instance.InsertItems(item, GameManager.instance.values[GameManager.instance.keys.IndexOf(item)], NivelId);
                }

                foreach (string item in GameManager.instance.noConoceIndex)
                {
                    SQLiteDB.instance.InsertConoce(item, GameManager.instance.noConoceValues[GameManager.instance.noConoceIndex.IndexOf(item)], NivelId);
                }
            }
        }
    }

    /// <summary>
    /// Cargar datos y estado de la partida
    /// </summary>
    /// <param name="s"></param>
    /// <param name="mode"></param>
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (partida != null && partida.Escena.Contains("End"))
        {
            return;
        }

        partida = SQLiteDB.instance.GetPartidaById(PlayerPrefs.GetInt("partidaId"));

        if (partida != null)
        {
            NivelId = SQLiteDB.instance.GetNivelId(Nivel, partida.Id);
        }
        

        if (NivelId != 0)
        {
            foreach (string item in GameManager.instance.keys)
            {
                GameManager.instance.values[GameManager.instance.keys.IndexOf(item)] = SQLiteDB.instance.Select("items", item, NivelId);
            }

            foreach (string item in GameManager.instance.noConoceIndex)
            {
                GameManager.instance.noConoceValues[GameManager.instance.noConoceIndex.IndexOf(item)] = SQLiteDB.instance.Select("conoce", item, NivelId);
            }
        }

        if ((partida != null && !SceneManager.GetActiveScene().name.Contains("End") && NivelId != 0) || !PauseMenuManager.instance.isPauseMenuActive)
        {
            Inventory.instance.Actualizar();
        }
    }

    /// <summary>
    /// Método para borrar todos los datos de la partida
    /// </summary>
    public void BorrarPartida()
    {
        SQLiteDB.instance.DeleteItems(NivelId);
        SQLiteDB.instance.DeleteConoce(NivelId);
        SQLiteDB.instance.DeleteCollect(partida.Id);
        SQLiteDB.instance.DeleteNivel(Nivel, partida.Id);
        SQLiteDB.instance.DeletePartida(partida.Id);
        partida = null;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Método para destruir la instancia del GameManager
    /// No se destruye sola ya que hemos definido el DontDestroyOnLoad en el Awake()
    /// </summary>
    public void Destroy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Calcular los puntos de la partida
    /// </summary>
    /// <returns></returns>
    private int Calcular()
    {
        int collected = SQLiteDB.instance.SelectCollected(partida.Id);

        int result = (collected * 20) - (partida.DeadEnds * 5);

        return result;
    }
}
