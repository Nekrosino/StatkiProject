using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    public float waterLevel = 0.0f; // Poziom wody, na kt�rym ma unosi� si� obiekt
    public float floatThreshold = 2.0f; // Wysoko��, na jakiej obiekt zaczyna unosi� si�
    public float waterDensity = 1.0f; // G�sto�� wody (wp�ywa na si�� wyporu)
    public float buoyancyForceFactor = 0.5f; // Wsp�czynnik si�y wyporu

    private MeshFilter meshFilter;
    private Vector3[] baseVertices;

    private Rigidbody rb;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        baseVertices = mesh.vertices;

        rb = GetComponent<Rigidbody>();
       
    }

    void FixedUpdate()
    {
        float time = Time.time;
        float highestPoint = float.MinValue;

        // Znajd� najwy�szy punkt siatki
        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            // Get vertex coordinates in the XY plane
            float x = vertex.x;
            float z = vertex.z;

            // Evaluate Bezier curve values for X and Z coordinates
            float curveValueX = GetComponent<BezierWaterWaves>().bezierCurveX.Evaluate(Mathf.Repeat(x / GetComponent<BezierWaterWaves>().waveLength, 1f));
            float curveValueZ = GetComponent<BezierWaterWaves>().bezierCurveZ.Evaluate(Mathf.Repeat(z / GetComponent<BezierWaterWaves>().waveLength, 1f));

            // Calculate vertex height including wave height
            float vertexHeight = Mathf.Sin(time * GetComponent<BezierWaterWaves>().waveSpeed + x * 0.5f + z * 0.3f) * curveValueX * curveValueZ * GetComponent<BezierWaterWaves>().waveHeight;

            // Keep track of the highest point
            if (vertexHeight > highestPoint)
            {
                highestPoint = vertexHeight;
            }
        }

        // Sprawd�, czy obiekt jest pod wod�
        if (highestPoint + transform.position.y < waterLevel)
        {
            // Oblicz odleg�o�� od powierzchni wody
            float distanceToWater = waterLevel - (highestPoint + transform.position.y);

            // Sprawd�, czy obiekt jest wystarczaj�co g��boko zanurzony, aby zacz�� unosi� si�
            if (distanceToWater < floatThreshold)
            {
                // Oblicz si�� wyporu
                float buoyancyForce = waterDensity * Mathf.Abs(Physics.gravity.y) * buoyancyForceFactor * distanceToWater;

                // Dodaj si�� wyporu do Rigidbody
                rb.AddForce(Vector3.up * buoyancyForce, ForceMode.Force);
            }
        }
    }
}
