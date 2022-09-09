using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase para definir el comportamiento de objetos con los que se pueda interactuar
/// </summary>
public class InteractableThing : Collectables
{
    FloatingText ft;
    FloatingText ft2;

    public string text;

    /// <summary>
    /// Acciones al entrar en contacto
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
    /// Acciones al presionar la tecla para interactuar
    /// </summary>
    /// <param name="coll"></param>
    protected override void OnInteractPressed(Collider2D coll)
    {
        ft2 = FloatingTextManager.instance.Show(text, 35, Color.white, transform.position, "Npc", new Vector3(), 0f);
        if (ft != null) { ft.Hide(); }
    }

    /// <summary>
    /// Acciones a realizar al entrar en colisión
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    /// <summary>
    /// Acciones a realizar al finalizar la colisión con el objeto
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collected = false;
            if (ft != null) { ft.Hide(); }
            if (ft2 != null) { ft2.Hide(); }
        }
    }
}
