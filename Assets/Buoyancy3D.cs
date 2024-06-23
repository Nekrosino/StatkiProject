using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy3D : MonoBehaviour
{
    public float waterLevel = 0.0f; // Poziom wody, na kt�rym ma unosi� si� obiekt
    public float floatThreshold = 2.0f; // Wysoko��, na jakiej obiekt zaczyna unosi� si�
    public float waterDensity = 1000.0f; // G�sto�� wody (wp�ywa na si�� wyporu)
    public float buoyancyForceFactor = 0.5f; // Wsp�czynnik si�y wyporu

    private Rigidbody rb;
    private bool isSubmerged = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // W��cz grawitacj�, domy�lnie obiekt jest pod grawitacj�
    }

    void FixedUpdate()
    {
        // Oblicz odleg�o�� do powierzchni wody
        float distanceToWater = waterLevel - transform.position.y;

        if (distanceToWater < floatThreshold)
        {
            // Oblicz si�� wyporu
            float buoyancyForce = waterDensity * Mathf.Abs(Physics.gravity.y) * buoyancyForceFactor * Mathf.Abs(distanceToWater);

            // Dodaj si�� wyporu do Rigidbody
            rb.AddForce(Vector3.up * buoyancyForce, ForceMode.Force);

            // Wy��cz grawitacj�, aby obiekt unosi� si� na wodzie
            rb.useGravity = false;

            // Ustaw flag�, �e obiekt jest zanurzony
            isSubmerged = true;
        }
        else
        {
            // W��cz grawitacj�, je�li obiekt jest powy�ej wody
            rb.useGravity = true;

            // Ustaw flag�, �e obiekt nie jest zanurzony
            isSubmerged = false;
        }
    }

    // Metoda wywo�ywana, gdy obiekt dotknie wody
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            // Przypisz poziom wody na podstawie pozycji obiektu w momencie dotkni�cia wody
            waterLevel = other.transform.position.y;
        }
    }

    // Metoda wywo�ywana, gdy obiekt opu�ci wod�
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            // Zresetuj poziom wody
            waterLevel = 0.0f;
        }
    }
}
