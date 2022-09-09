using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;


/// <summary>
/// Script para controlar el funcionamiento de los NPCs
/// </summary>
public class Npc : Collectables
{
    FloatingText ft;

    /// <summary>
    /// json con el diálogo del npc y sus opciones correspondientes
    /// </summary>
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    /// <summary>
    /// Acciones al colisionar con el NPC
    /// </summary>
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;

            ft = FloatingTextManager.instance.Show("I", 35, Color.white, transform.position, "Npc", new Vector3(), 0f);
        }
    }

    /// <summary>
    /// Acciones al interactuar con el NPC
    /// </summary>
    /// <param name="coll"></param>
    protected override void OnInteractPressed(Collider2D coll)
    {
        DialogueManger.instance.EnterDialogueMode(inkJSON);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    /// <summary>
    /// Acciones al terminar la colisión con el NPC
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
