using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase intermedia para objetos que adem�s de poder colisionar con ellos se puedan recoger
/// Los m�todos son para sobreescribirlos en las clases que hereden de Collectable
/// </summary>
public class Collectables : Collidable_object
{
    protected bool collected;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Acros") {OnCollect();}
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
