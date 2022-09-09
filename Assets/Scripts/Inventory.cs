using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// Script para controlar el funcionamiento del inventario
/// </summary>
public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<GameObject> slots;

    private List<bool> states;

    public List<Sprite> sprites;

    /// <summary>
    /// Al inicializar seteamos el estado a false para no mostrar ningun hueco de inventario activo
    /// </summary>
    private void Awake()
    {
        if (Inventory.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        states = new List<bool>();
        foreach(var slot in slots)
        {
            slot.SetActive(false);
            states.Add(false);
        }
    }

    /// <summary>
    /// Actualiza los valores y huecos del inventario en función de los que tenga el jugador
    /// </summary>
    public void Actualizar()
    {
        for (int i = 0 ; i <  GameManager.instance.keys.Count ; i++)
        {
            if (GameManager.instance.values[i] != 0)
            {
                slots[i].SetActive(true);
                GameObject.FindGameObjectWithTag("inventory" + (i + 1)).GetComponent<SpriteRenderer>().sprite = sprites[i];
                GameObject.FindGameObjectWithTag("c" + (i + 1)).GetComponent<UnityEngine.UI.Text>().text = GameManager.instance.values[i].ToString();
                states[i] = true;
            } else
            {
                slots[i].SetActive(false);
                states[i] = false;
            }
        }
    }
}
