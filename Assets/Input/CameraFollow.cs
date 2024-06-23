using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform target; // Referencja do transformacji gracza, kt�r� b�dziemy �ledzi�
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Offset kamery wzgl�dem gracza
    public float smoothSpeed = 0.5f; // Szybko��, z jak� kamera �ledzi gracza

    void LateUpdate()
    {
        if (target == null)
            return;

        // Oblicz docelow� pozycj� kamery na podstawie pozycji gracza i offsetu
        Vector3 desiredPosition = target.position + offset;

        // Wyg�ad� ruch kamery za pomoc� funkcji Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Ustaw now� pozycj� kamery
        transform.position = smoothedPosition;

        // Skieruj kamer� w stron� gracza
        transform.LookAt(target);
    }
}
