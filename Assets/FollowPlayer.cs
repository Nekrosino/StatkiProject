using UnityEngine;

public class ShipCameraController : MonoBehaviour
{
    public Transform target;  // Obiekt, wok� kt�rego obracamy kamer� (np. statek)
    public float rotationSpeed = 1f;  // Pr�dko�� obrotu kamery
    public float distanceOffset = 10f;  // Dodatkowy offset odleg�o�ci kamery od statku

    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        // Pobierz ruch myszy (lub joysticka) do obrotu kamery
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Aktualizuj k�ty yaw i pitch na podstawie ruchu myszy
        yaw += mouseX * rotationSpeed;
        pitch -= mouseY * rotationSpeed;

        // Ogranicz zakres pitch, aby kamera nie mog�a si� obr�ci� o wi�cej ni� 90 stopni w g�r� i w d�
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Stw�rz Quaternion na podstawie obrotu yaw i pitch
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Zaktualizuj pozycj� kamery na podstawie obrotu i odleg�o�ci od celu (statku) z uwzgl�dnieniem offsetu
        transform.position = target.position - rotation * Vector3.forward * (distanceOffset + 10f);  // Ustaw odleg�o�� kamery od statku
        transform.rotation = rotation;  // Ustaw obr�t kamery

        // Patrz zawsze na cel (statku)
        transform.LookAt(target);
    }
}