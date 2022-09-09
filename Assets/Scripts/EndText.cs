using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Clase para controlar el texto que se muestra al final de la partida
/// junto con las acciones a llevar a cabo después
/// </summary>
public class EndText : MonoBehaviour
{
    public static EndText instance;
    private bool Ended = false;

    private void Awake()
    {
        instance = this;
        GetComponent<Text>().enabled = false;
        GameManager.instance.partida.Escena = SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// Mostrar texto
    /// </summary>
    public void EndIt()
    {
        if (!Ended)
        {
            Ended = true;
            GetComponent<Text>().enabled = true;
            StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
        }
    }

    /// <summary>
    /// Cuando se haya mostrado el texto, con la barra de espacion guardaremos los datos y volveremos a la pantalla principal
    /// </summary>
    void Update()
    {
        if (Ended && Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.Destroy();
            SceneManager.LoadScene("MainMenu");
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
}
