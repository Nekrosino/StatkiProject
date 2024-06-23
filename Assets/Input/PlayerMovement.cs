using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveAmount;
    bool isJumping;

    public void OnMove(InputAction.CallbackContext context)
    {
        // Odczytaj wartoœæ dla akcji "Move" przy ka¿dym wywo³aniu eventu
        moveAmount = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // SprawdŸ, czy akcja "Jump" zosta³a uruchomiona
        if (context.started)
        {
            // Kod skoku postaci
            Jump();
        }
    }

    void Jump()
    {
        // Implementacja skoku postaci
        Debug.Log("Jumping!");
    }

    void Update()
    {
        // U¿yj wartoœci Vector2 z akcji "Move" w ka¿dej klatce
        Vector3 moveDirection = new Vector3(moveAmount.x, 0, moveAmount.y);
        transform.Translate(moveDirection * Time.deltaTime * 5f); // Przyk³adowe poruszanie postaci¹
    }
}
