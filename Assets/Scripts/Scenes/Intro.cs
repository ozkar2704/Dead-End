using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase para mostrar el texto de la pantalla de inicio de la partida
/// </summary>
public class Intro : MonoBehaviour
{
    public List<string> textos;
    private int contador = 0;

    /// <summary>
    /// Al iniciarlizar la clase cojemos el primer texto e iniciamos el "fade in" del mismo
    /// </summary>
    private void Awake()
    {
        GetComponent<Text>().text = textos[contador];
        contador++;
        StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
    }

    /// <summary>
    /// Al presionar la tecla de espacio actualizamos el texto al siguente con el correspondiente "fade in"
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
            //System.Threading.Thread.Sleep(1000);
            if (textos.Count > contador)
            {
                GetComponent<Text>().text = textos[contador];
                contador++;
                StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
            } else
            {
                PlayerPrefs.SetString("escena","Start");
                SceneManager.LoadScene("Start");
            }
        }
    }

    /// <summary>
    /// Hacer "fade in" del texto proporcionado
    /// </summary>
    /// <param name="t"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    /// <summary>
    /// Hacer "fade out" del texto proporcionado
    /// </summary>
    /// <param name="t"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;//new WaitForSeconds(0.1f);
        }
    }
}