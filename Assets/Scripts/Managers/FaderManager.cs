using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase para controlar el "fade out" de la pantalla en negro al cambiar de escenas
/// </summary>
public class FaderManager : MonoBehaviour
{
    public static FaderManager instance;
    public Animator animator;
    
    private void Awake()
    {
        if (FaderManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    /// <summary>
    /// Cambia el valor del trigger en el animador para inciar la animación de "fade out"
    /// </summary>
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
}
