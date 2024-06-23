using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform target; // Referencja do transformacji gracza, któr¹ bêdziemy œledziæ
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Offset kamery wzglêdem gracza
    public float smoothSpeed = 0.5f; // Szybkoœæ, z jak¹ kamera œledzi gracza

    void LateUpdate()
    {
        if (target == null)
            return;

        // Oblicz docelow¹ pozycjê kamery na podstawie pozycji gracza i offsetu
        Vector3 desiredPosition = target.position + offset;

        // Wyg³adŸ ruch kamery za pomoc¹ funkcji Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Ustaw now¹ pozycjê kamery
        transform.position = smoothedPosition;

        // Skieruj kamerê w stronê gracza
        transform.LookAt(target);
    }
}
