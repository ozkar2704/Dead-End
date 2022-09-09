using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script para cofres
/// </summary>
public class Chest : Collectables
{
    //Sprite para mostrar cofre vacío
    public Sprite emptyChest;
    //Variable pública modificable desde el editor para indicar la cantidad de oro que proporciona el cofre
    public int dinero = 5;

    /// <summary>
    /// Método para inicializar el estado del cofre en base a si ya se ha recogido antes o no
    /// </summary>
    private void Awake()
    {
        if (SQLiteDB.instance.IsCollected("chest", SceneManager.GetActiveScene().name, PlayerPrefs.GetInt("partidaId")))
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
        }
    }

    /// <summary>
    /// Entrega dinero al jugador, actualiza inventario y guarda la información necesaria en la base de datos
    /// </summary>
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            SQLiteDB.instance.SetCollected("chest", SceneManager.GetActiveScene().name, GameManager.instance.partida.Id);
            GameManager.instance.values[GameManager.instance.keys.FindIndex(x => x.Contains("gold"))] += dinero;
            FloatingTextManager.instance.Show("+" + dinero + " oro", 15, Color.yellow, transform.position, "Chest", Vector3.up * 50, 0.6f);

            Inventory.instance.Actualizar();
        }      
    }
}
