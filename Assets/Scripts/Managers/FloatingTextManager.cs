using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador del FloatingText
/// </summary>
public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    public static FloatingTextManager instance;

    private void Awake()
    {
        if (FloatingTextManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    /// <summary>
    /// Obtiene el texto
    /// </summary>
    /// <returns></returns>
    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }

    /// <summary>
    /// Actualiza el texto
    /// </summary>
    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }

    /// <summary>
    /// Muestra el texto
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="fontSize"></param>
    /// <param name="color"></param>
    /// <param name="position"></param>
    /// <param name="lookAt"></param>
    /// <param name="motion"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public FloatingText Show(string msg, int fontSize, Color color, Vector3 position, string lookAt, Vector2 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        floatingText.tag = lookAt;
        if (duration > 0)
        {
            floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);//Transfer world space to screeen space so we can use it in the UI
        }
        else
        {
            floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position + FloatingText.offset);//El offset es indica la posición e la que mostrar el texto
        }
        floatingText.motion = motion;
        floatingText.duration = duration;
        floatingText.Show();
        return floatingText;
    }
}

