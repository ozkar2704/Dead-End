using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script para ocntrolar el movimiento del jugador
/// </summary>
public class Player_movement : MonoBehaviour
{
    private Vector3 moveDelta;
    public Animator animator;
    private bool isMoving;

    /// <summary>
    /// Actualizar movimiento del personaje junto con la animación correspondiente
    /// </summary>
    private void FixedUpdate()
    {
    	float x = Input.GetAxisRaw("Horizontal");
    	float y = Input.GetAxisRaw("Vertical");

    	//Reset moveDelta
    	moveDelta = new Vector3(x, y, 0);

        //No se puede mover en caso de que el menú o un diálogo estén activos
        if (!PauseMenuManager.instance.isPauseMenuActive && (DialogueManger.instance == null || !DialogueManger.instance.dialogueIsPlaying))
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);

            if (x != 0 || y != 0)
            {
                //TODO: Implementar animaciones en diagonal
                animator.SetFloat("X", x);
                animator.SetFloat("Y", y);
                if (!isMoving)
                {
                    isMoving = true;
                    animator.SetBool("IsMoving", isMoving);
                }
            } else
            {
                if (isMoving)
                {
                    isMoving = false;
                    animator.SetBool("IsMoving", isMoving);
                }
            }
        }
    }
}
