using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que detecta las colisiones de los objetos que usen el script
/// </summary>
public class Collidable_object : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); 
    }

    /// <summary>
    /// Detectamos cuando ha habido una colisión y con qué ha sido la colisión
    /// Este método se puede sobre escribir en clases que hereden de la clase Collidable_object
    /// </summary>
    protected virtual void Update()
    {
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null || hits[i].name.Equals("Mid")){continue;}

            OnCollide(hits[i]);

            if (InputManager.GetInstance().GetInteractPressed() && !DialogueManger.instance.dialogueIsPlaying)
            {
                OnInteractPressed(hits[i]);
            }

            hits[i] = null;
        }
    }

    /// <summary>
    /// Describe que acciones realizar al colisionar
    /// Este método es para sobreescribirlo en las clases que hereden Collidable_object
    /// </summary>
    /// <param name="coll"></param>
    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log(coll.name);
    }

    /// <summary>
    /// Describe que acciones realizar cuando el jugador presione interactuar estando en colisión con un objeto
    /// Este método es para sobreescribirlo en las clases que hereden Collidable_object
    /// </summary>
    /// <param name="coll"></param>
    protected virtual void OnInteractPressed(Collider2D coll)
    {
        Debug.Log(coll.name);
    }
}
