using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script para el control de movimiento de la cámara
/// </summary>
public class CameraControl : MonoBehaviour
{
    //lookAt especifica el objeto al que sigue la cámara, en nuestro caso el personaje principal
    public Transform lookAt;
    //Los siguientes parámetros indican el margen que tiene la cámara antes de moverse
    public float boundX = 0.15f;
    public float boundY = 0.15f;

    /// <summary>
    /// Método que actualiza el movimiento de la cámara
    /// </summary>
    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            } else {
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            } else {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
