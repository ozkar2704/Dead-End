using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script para transportar personaje a siguiente escena
/// </summary>
public class Portal : Collidable_object
{
    public string sceneName;

    /// <summary>
    /// Guardar partida, "fade out" y cambiar de escena al colisionar
    /// </summary>
    /// <param name="coll"></param>
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Acros")
        {
            //Teleport
            GameManager.instance.SaveState();
            FaderManager.instance.FadeOut();

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
