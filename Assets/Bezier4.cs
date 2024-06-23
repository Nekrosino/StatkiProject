using UnityEngine;

public class Bezier4 : MonoBehaviour
{
    public float waveHeight = 0.5f;
    public float waveSpeed = 1f;
    public float waveLength = 2f;
    public AnimationCurve bezierCurveX;
    public AnimationCurve bezierCurveZ;

    private MeshFilter[] meshFilters;
    private Vector3[][] baseVertices;
    private Vector3[][] displacedVertices;
    private MeshCollider[] meshColliders;

    void Start()
    {
        // Pobierz wszystkie obiekty z tagiem "Water" w scenie
        GameObject[] waterObjects = GameObject.FindGameObjectsWithTag("Water");

        // Inicjalizuj tablice na podstawie liczby znalezionych obiektów z tagiem "Water"
        meshFilters = new MeshFilter[waterObjects.Length];
        baseVertices = new Vector3[waterObjects.Length][];
        displacedVertices = new Vector3[waterObjects.Length][];
        meshColliders = new MeshCollider[waterObjects.Length];

        // Iteruj przez wszystkie obiekty z tagiem "Water" i inicjalizuj tablice i komponenty
        for (int i = 0; i < waterObjects.Length; i++)
        {
            meshFilters[i] = waterObjects[i].GetComponent<MeshFilter>();
            if (meshFilters[i] != null)
            {
                Mesh mesh = meshFilters[i].mesh;
                baseVertices[i] = mesh.vertices;
                displacedVertices[i] = new Vector3[baseVertices[i].Length];

                meshColliders[i] = waterObjects[i].GetComponent<MeshCollider>();
                if (meshColliders[i] == null)
                {
                    meshColliders[i] = waterObjects[i].AddComponent<MeshCollider>();
                }
            }
            else
            {
                Debug.LogError("MeshFilter component not found on object with tag Water");
            }
        }
    }

    void Update()
    {
        float time = Time.time * waveSpeed;

        for (int plane = 0; plane < meshFilters.Length; plane++)
        {
            if (meshFilters[plane] != null && meshFilters[plane].mesh != null &&
                meshColliders[plane] != null)
            {
                for (int i = 0; i < baseVertices[plane].Length; i++)
                {
                    Vector3 vertex = baseVertices[plane][i];

                    // Get vertex coordinates in the XY plane
                    float x = vertex.x;
                    float z = vertex.z;

                    // Evaluate Bezier curve values for X and Z coordinates
                    float curveValueX = bezierCurveX.Evaluate(Mathf.Repeat(x / waveLength, 1f));
                    float curveValueZ = bezierCurveZ.Evaluate(Mathf.Repeat(z / waveLength, 1f));

                    // Apply waves based on Bezier curves
                    vertex.y = Mathf.Sin(time + x * 0.5f + z * 0.3f) * curveValueX * curveValueZ * waveHeight;

                    displacedVertices[plane][i] = vertex;
                }

                // Update mesh vertices and normals for each plane
                meshFilters[plane].mesh.vertices = displacedVertices[plane];
                meshFilters[plane].mesh.RecalculateNormals();

                // Update MeshCollider with the modified mesh
                meshColliders[plane].sharedMesh = null;
                meshColliders[plane].sharedMesh = meshFilters[plane].mesh;
            }
            else
            {
                Debug.LogError("MeshFilter, MeshCollider or their mesh is null for plane " + plane);
            }
        }
    }
}