using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script para controlar el funcionamiento de los items
/// </summary>
public class Item : Collectables
{
    FloatingText ft;
    public string lookAt;

    /// <summary>
    /// Al inicializar mostramos o no el item en función de si el jugador ya lo ha recogido o no
    /// </summary>
    private void Awake()
    {
        if (SQLiteDB.instance.IsCollected(lookAt, SceneManager.GetActiveScene().name, PlayerPrefs.GetInt("partidaId")))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    /// <summary>
    /// Acciones al colisionar con el objeto
    /// </summary>
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;

            ft = FloatingTextManager.instance.Show("I", 35, Color.white, transform.position, lookAt, new Vector3(), 0f);

            Inventory.instance.Actualizar();
        }
    }

    /// <summary>
    /// Acciones al interactuar con el objeto
    /// </summary>
    /// <param name="coll"></param>
    protected override void OnInteractPressed(Collider2D coll)
    {
        if (collected)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            SQLiteDB.instance.SetCollected(lookAt, SceneManager.GetActiveScene().name, GameManager.instance.partida.Id);
            GameManager.instance.values[GameManager.instance.keys.FindIndex(x => x.Contains(lookAt))] += 1;
            FloatingTextManager.instance.Show("+ 1 " + lookAt, 15, Color.red, transform.position, lookAt, Vector3.up * 50, 0.6f);

            Inventory.instance.Actualizar();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    /// <summary>
    /// Acciones al finalizar la colisión con el objeto
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collected = false;
            if (ft != null) { ft.Hide(); }
        }
    }
}
