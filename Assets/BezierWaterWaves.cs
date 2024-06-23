/*
using UnityEngine;

public class BezierWaterWaves : MonoBehaviour
{
    public float waveHeight = 0.5f;
    public float waveSpeed = 1f;
    public float waveLength = 2f;
    public AnimationCurve bezierCurveX;
    public AnimationCurve bezierCurveZ;

    private MeshFilter meshFilter;
    private Vector3[] baseVertices;
    private Vector3[] displacedVertices;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        baseVertices = mesh.vertices;
        displacedVertices = new Vector3[baseVertices.Length];
    }

    void Update()
    {
        float time = Time.time * waveSpeed;
        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            // Get vertex coordinates in the XY plane
            float x = vertex.x;
            float z = vertex.z;

            // Evaluate Bezier curve values for X and Z coordinates
            float curveValueX = bezierCurveX.Evaluate(Mathf.Repeat(x / waveLength, 1f));
            float curveValueZ = bezierCurveZ.Evaluate(Mathf.Repeat(z / waveLength, 1f));

            // Apply waves based on Bezier curves
            vertex.y = Mathf.Sin(time + x * 0.5f + z * 0.3f) * curveValueX * curveValueZ * waveHeight;

            displacedVertices[i] = vertex;
        }

        meshFilter.mesh.vertices = displacedVertices;
        meshFilter.mesh.RecalculateNormals();
    }
}

*/ 
using UnityEngine;

public class MultiBezierWaterWaves : MonoBehaviour
{
    public float waveHeight = 0.5f;
    public float waveSpeed = 1f;
    public float waveLength = 2f;
    public AnimationCurve[] bezierCurvesX; // Tablica krzywych Béziera dla X
    public AnimationCurve[] bezierCurvesZ; // Tablica krzywych Béziera dla Z

    private MeshFilter[] meshFilters; // Tablica filtrów siatek
    private Vector3[][] baseVertices; // Tablica bazowych wierzcho³ków
    private Vector3[][] displacedVertices; // Tablica wierzcho³ków przemieszczonych

    void Start()
    {
        // Inicjalizacja tablic i pobranie siatek
        meshFilters = new MeshFilter[4]; // Zak³adam 4 p³aty
        baseVertices = new Vector3[4][];
        displacedVertices = new Vector3[4][];

        for (int i = 0; i < 4; i++)
        {
            meshFilters[i] = transform.GetChild(i).GetComponent<MeshFilter>(); // Pobierz ka¿dy MeshFilter jako dziecko obiektu
            Mesh mesh = meshFilters[i].mesh;
            baseVertices[i] = mesh.vertices;
            displacedVertices[i] = new Vector3[baseVertices[i].Length];
        }
    }

    void Update()
    {
        float time = Time.time * waveSpeed;

        // Iteracja po ka¿dym p³acie
        for (int plat = 0; plat < 4; plat++)
        {
            Vector3[] baseVerts = baseVertices[plat];
            Vector3[] displacedVerts = displacedVertices[plat];

            for (int i = 0; i < baseVerts.Length; i++)
            {
                Vector3 vertex = baseVerts[i];

                // Pobierz wspó³rzêdne X i Z wierzcho³ka
                float x = vertex.x;
                float z = vertex.z;

                // Uzyskaj wartoœæ krzywych Béziera dla X i Z dla danego p³atu
                float curveValueX = bezierCurvesX[plat].Evaluate(Mathf.Repeat(x / waveLength, 1f));
                float curveValueZ = bezierCurvesZ[plat].Evaluate(Mathf.Repeat(z / waveLength, 1f));

                // Zastosowanie fal na podstawie krzywych Béziera dla danego p³atu
                vertex.y = Mathf.Sin(time + x * 0.5f + z * 0.3f) * curveValueX * curveValueZ * waveHeight;

                displacedVerts[i] = vertex;
            }

            // Zaktualizuj siatkê danego p³atu
            Mesh mesh = meshFilters[plat].mesh;
            mesh.vertices = displacedVerts;
            mesh.RecalculateNormals();
        }
    }
}