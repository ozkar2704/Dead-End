using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Floating text define objetos tipo texto que se muestran encima de los GO que los usen
/// </summary>
public class FloatingText
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float lastShown;
    public float duration;
    public string tag;
    public GameObject lookAt;
    public static Vector3 offset = new Vector3(0, 0.2f, 0);

    /// <summary>
    /// Muestra el texto
    /// </summary>
    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    /// <summary>
    /// Esconde el texto
    /// </summary>
    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    /// <summary>
    /// Cambia el texto
    /// </summary>
    /// <param name="text"></param>
    public void ChangeText(string text)
    {
        txt.text = text;
    }

    /// <summary>
    /// Actualiza el texto
    /// </summary>
    public void UpdateFloatingText()
    {
        if (!active) { return; }

        if (duration > 0)
        {
            if (Time.time - lastShown > duration) { Hide(); }

            go.transform.position += motion * Time.deltaTime;
        } else
        {
            lookAt = GameObject.FindGameObjectWithTag(tag);
            if (lookAt != null)
            {
                go.transform.position = Camera.main.WorldToScreenPoint(lookAt.transform.position + offset);
            }
        }
    }
}
