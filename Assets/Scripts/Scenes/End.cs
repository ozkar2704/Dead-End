using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Clase para controlar lo que sucede cuando el jugador llega al final del juego
/// </summary>
public class End : Collidable_object
{
    /// <summary>
    /// Realizamos acciones cuando llegue al punto del final del juego
    /// </summary>
    /// <param name="coll"></param>
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Acros")
        {
            FaderManager.instance.FadeOut();

            EndText.instance.EndIt();
        }
    }

    
}
