using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Clase para describir lo que sucede cuando el jugador llegue a un "Dead End"
/// </summary>
public class DeadEnd : MonoBehaviour
{
    private int timer = 0;
    private bool stop = false;

    /// <summary>
    /// Al inicializar deshabilitamos el texto y registramos en que escena nos encontramos
    /// </summary>
    private void Awake()
    {
        GetComponent<Text>().enabled = false;
        GameManager.instance.partida.Escena = SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// Después de un tiempo fijo, mostramos el mensaje y ponemos la pantalla en negro
    /// Cuando se pulse la barra de espacio guardaremos la partida y volveremos al menú inicial
    /// </summary>
    void Update()
    {
        if (timer > 750 && !stop)
        {
            stop = true;
            FaderManager.instance.FadeOut();
            GetComponent<Text>().enabled = true;
            StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
        } else
        {
            timer++;
        }

        if (stop && Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.Destroy();
            SceneManager.LoadScene("MainMenu");
        }
    }

    /// <summary>
    /// Realizar "fade in" del texto
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
