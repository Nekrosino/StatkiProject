using UnityEngine;

public class Bezier : MonoBehaviour
{
    public float waveHeight = 0.5f;
    public float waveSpeed = 1f;
    public float waveLength = 2f;
    public AnimationCurve[] bezierCurvesX; // Tablica krzywych Béziera dla X
    public AnimationCurve[] bezierCurvesZ; // Tablica krzywych Béziera dla Z

    private MeshFilter[] meshFilters; // Tablica filtrów siatek
    private MeshCollider[] meshColliders; // Tablica colliderów siatek
    private Vector3[][] baseVertices; // Tablica bazowych wierzchołków
    private Vector3[][] displacedVertices; // Tablica wierzchołków przemieszczonych

    void Start()
    {
        meshFilters = new MeshFilter[4];
        meshColliders = new MeshCollider[4];
        baseVertices = new Vector3[4][];
        displacedVertices = new Vector3[4][];

        // Przykład bezpiecznego pobierania komponentów
        for (int i = 0; i < 4; i++)
        {
            // Pobierz MeshFilter
            meshFilters[i] = transform.GetChild(i).GetComponent<MeshFilter>();
            if (meshFilters[i] == null)
            {
                Debug.LogError("MeshFilter not found on child object " + i);
                return; // przerwij działanie metody, jeśli komponent nie został znaleziony
            }

            // Pobierz MeshCollider
            meshColliders[i] = transform.GetChild(i).GetComponent<MeshCollider>();
            if (meshColliders[i] == null)
            {
                Debug.LogError("MeshCollider not found on child object " + i);
                return; // przerwij działanie metody, jeśli komponent nie został znaleziony
            }

            // Inicjalizacja tablic wierzchołków
            Mesh mesh = meshFilters[i].mesh;
            baseVertices[i] = mesh.vertices;
            displacedVertices[i] = new Vector3[baseVertices[i].Length];
        }
    }

    void Update()
    {
        float time = Time.time * waveSpeed;

        for (int plat = 0; plat < 4; plat++)
        {
            Vector3[] baseVerts = baseVertices[plat];
            Vector3[] displacedVerts = displacedVertices[plat];

            for (int i = 0; i < baseVerts.Length; i++)
            {
                Vector3 vertex = baseVerts[i];
                float x = vertex.x;
                float z = vertex.z;

                // Uzyskaj wartość krzywych Béziera dla X i Z
                float curveValueX = bezierCurvesX[plat].Evaluate(Mathf.Repeat(x / waveLength, 1f));
                float curveValueZ = bezierCurvesZ[plat].Evaluate(Mathf.Repeat(z / waveLength, 1f));

                // Zastosowanie fal na podstawie krzywych Béziera
                vertex.y = Mathf.Sin(time + x * 0.5f + z * 0.3f) * curveValueX * curveValueZ * waveHeight;

                displacedVerts[i] = vertex;
            }

            // Zaktualizuj siatkę i collider
            Mesh mesh = meshFilters[plat].mesh;
            mesh.vertices = displacedVerts;
            mesh.RecalculateNormals();

            meshColliders[plat].sharedMesh = mesh; // Aktualizuj collider
        }
    }
}
