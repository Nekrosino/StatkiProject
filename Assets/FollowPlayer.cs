using UnityEngine;

public class ShipCameraController : MonoBehaviour
{
    public Transform target;  // Obiekt, wokó³ którego obracamy kamerê (np. statek)
    public float rotationSpeed = 1f;  // Prêdkoœæ obrotu kamery
    public float distanceOffset = 10f;  // Dodatkowy offset odleg³oœci kamery od statku

    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        // Pobierz ruch myszy (lub joysticka) do obrotu kamery
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Aktualizuj k¹ty yaw i pitch na podstawie ruchu myszy
        yaw += mouseX * rotationSpeed;
        pitch -= mouseY * rotationSpeed;

        // Ogranicz zakres pitch, aby kamera nie mog³a siê obróciæ o wiêcej ni¿ 90 stopni w górê i w dó³
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Stwórz Quaternion na podstawie obrotu yaw i pitch
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Zaktualizuj pozycjê kamery na podstawie obrotu i odleg³oœci od celu (statku) z uwzglêdnieniem offsetu
        transform.position = target.position - rotation * Vector3.forward * (distanceOffset + 10f);  // Ustaw odleg³oœæ kamery od statku
        transform.rotation = rotation;  // Ustaw obrót kamery

        // Patrz zawsze na cel (statku)
        transform.LookAt(target);
    }
}