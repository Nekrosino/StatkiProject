using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Prêdkoœæ poruszania siê statkiem
    public float rotationSpeed = 100f; // Prêdkoœæ obracania statkiem

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Pobierz komponent Rigidbody statku
    }

    void FixedUpdate()
    {
        // Pobierz wejœcie klawiatury w osiach X (lewo/prawo) i Z (przód/ty³)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Stwórz wektor ruchu na podstawie wejœcia klawiatury
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Zastosuj si³ê do Rigidbody statku, aby siê porusza³
        rb.AddForce(movement * speed);

        // Oblicz obrót statku na podstawie wejœcia klawiatury
        float rotation = moveHorizontal * rotationSpeed * Time.deltaTime;

        // Zastosuj obrót do Rigidbody statku
        rb.rotation *= Quaternion.Euler(0, rotation, 0);
    }
}
