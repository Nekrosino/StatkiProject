using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveAmount;
    bool isJumping;

    public void OnMove(InputAction.CallbackContext context)
    {
        // Odczytaj warto�� dla akcji "Move" przy ka�dym wywo�aniu eventu
        moveAmount = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Sprawd�, czy akcja "Jump" zosta�a uruchomiona
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
        // U�yj warto�ci Vector2 z akcji "Move" w ka�dej klatce
        Vector3 moveDirection = new Vector3(moveAmount.x, 0, moveAmount.y);
        transform.Translate(moveDirection * Time.deltaTime * 5f); // Przyk�adowe poruszanie postaci�
    }
}
