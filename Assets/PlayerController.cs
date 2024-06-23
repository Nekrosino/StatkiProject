using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Pr�dko�� poruszania si� statkiem
    public float rotationSpeed = 100f; // Pr�dko�� obracania statkiem

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Pobierz komponent Rigidbody statku
    }

    void FixedUpdate()
    {
        // Pobierz wej�cie klawiatury w osiach X (lewo/prawo) i Z (prz�d/ty�)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Stw�rz wektor ruchu na podstawie wej�cia klawiatury
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Zastosuj si�� do Rigidbody statku, aby si� porusza�
        rb.AddForce(movement * speed);

        // Oblicz obr�t statku na podstawie wej�cia klawiatury
        float rotation = moveHorizontal * rotationSpeed * Time.deltaTime;

        // Zastosuj obr�t do Rigidbody statku
        rb.rotation *= Quaternion.Euler(0, rotation, 0);
    }
}
