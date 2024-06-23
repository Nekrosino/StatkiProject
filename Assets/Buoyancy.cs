using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    public float waterLevel = 0.0f; // Poziom wody, na którym ma unosiæ siê obiekt
    public float floatThreshold = 2.0f; // Wysokoœæ, na jakiej obiekt zaczyna unosiæ siê
    public float waterDensity = 1.0f; // Gêstoœæ wody (wp³ywa na si³ê wyporu)
    public float buoyancyForceFactor = 0.5f; // Wspó³czynnik si³y wyporu

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

        // ZnajdŸ najwy¿szy punkt siatki
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

        // SprawdŸ, czy obiekt jest pod wod¹
        if (highestPoint + transform.position.y < waterLevel)
        {
            // Oblicz odleg³oœæ od powierzchni wody
            float distanceToWater = waterLevel - (highestPoint + transform.position.y);

            // SprawdŸ, czy obiekt jest wystarczaj¹co g³êboko zanurzony, aby zacz¹æ unosiæ siê
            if (distanceToWater < floatThreshold)
            {
                // Oblicz si³ê wyporu
                float buoyancyForce = waterDensity * Mathf.Abs(Physics.gravity.y) * buoyancyForceFactor * distanceToWater;

                // Dodaj si³ê wyporu do Rigidbody
                rb.AddForce(Vector3.up * buoyancyForce, ForceMode.Force);
            }
        }
    }
}
