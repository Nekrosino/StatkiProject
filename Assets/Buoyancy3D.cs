using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy3D : MonoBehaviour
{
    public float waterLevel = 0.0f; // Poziom wody, na którym ma unosiæ siê obiekt
    public float floatThreshold = 2.0f; // Wysokoœæ, na jakiej obiekt zaczyna unosiæ siê
    public float waterDensity = 1000.0f; // Gêstoœæ wody (wp³ywa na si³ê wyporu)
    public float buoyancyForceFactor = 0.5f; // Wspó³czynnik si³y wyporu

    private Rigidbody rb;
    private bool isSubmerged = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // W³¹cz grawitacjê, domyœlnie obiekt jest pod grawitacj¹
    }

    void FixedUpdate()
    {
        // Oblicz odleg³oœæ do powierzchni wody
        float distanceToWater = waterLevel - transform.position.y;

        if (distanceToWater < floatThreshold)
        {
            // Oblicz si³ê wyporu
            float buoyancyForce = waterDensity * Mathf.Abs(Physics.gravity.y) * buoyancyForceFactor * Mathf.Abs(distanceToWater);

            // Dodaj si³ê wyporu do Rigidbody
            rb.AddForce(Vector3.up * buoyancyForce, ForceMode.Force);

            // Wy³¹cz grawitacjê, aby obiekt unosi³ siê na wodzie
            rb.useGravity = false;

            // Ustaw flagê, ¿e obiekt jest zanurzony
            isSubmerged = true;
        }
        else
        {
            // W³¹cz grawitacjê, jeœli obiekt jest powy¿ej wody
            rb.useGravity = true;

            // Ustaw flagê, ¿e obiekt nie jest zanurzony
            isSubmerged = false;
        }
    }

    // Metoda wywo³ywana, gdy obiekt dotknie wody
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            // Przypisz poziom wody na podstawie pozycji obiektu w momencie dotkniêcia wody
            waterLevel = other.transform.position.y;
        }
    }

    // Metoda wywo³ywana, gdy obiekt opuœci wodê
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            // Zresetuj poziom wody
            waterLevel = 0.0f;
        }
    }
}
