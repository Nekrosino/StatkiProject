using UnityEngine;
using UnityEngine.UIElements;

public class BezierTest : MonoBehaviour
{
    public static BezierTest instance;

    public float waveHeight = 0.5f;
    public float waveSpeed = 1f;
    public float waveLength = 2f;
    public AnimationCurve[] bezierCurvesX; // Tablica krzywych B�ziera dla X
    public AnimationCurve[] bezierCurvesZ; // Tablica krzywych B�ziera dla Z

    private MeshFilter[] meshFiltersArray; // Tablica filtr�w siatek
    private Vector3[][] baseVerticesArray; // Tablica bazowych wierzcho�k�w
    private Vector3[][] displacedVerticesArray; // Tablica wierzcho�k�w przemieszczonych

    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Pobierz liczb� dzieci (p�at�w)
        int numChildren = transform.childCount;

        // Inicjalizacja tablic i pobranie siatek
        meshFiltersArray = new MeshFilter[numChildren];
        baseVerticesArray = new Vector3[numChildren][];
        displacedVerticesArray = new Vector3[numChildren][];

        for (int i = 0; i < numChildren; i++)
        {
            meshFiltersArray[i] = transform.GetChild(i).GetComponent<MeshFilter>(); // Pobierz ka�dy MeshFilter jako dziecko obiektu
            Mesh mesh = meshFiltersArray[i].mesh;
            baseVerticesArray[i] = mesh.vertices;
            displacedVerticesArray[i] = new Vector3[baseVerticesArray[i].Length];
        }
    }

    void Update()
    {
        float time = Time.time * waveSpeed;

        // Iteracja po ka�dym p�acie
        for (int plat = 0; plat < meshFiltersArray.Length; plat++)
        {
            Vector3[] baseVerts = baseVerticesArray[plat];
            Vector3[] displacedVerts = displacedVerticesArray[plat];

            for (int i = 0; i < baseVerts.Length; i++)
            {
                Vector3 vertex = baseVerts[i];

                // Pobierz wsp�rz�dne X i Z wierzcho�ka
                float x = vertex.x;
                float z = vertex.z;

                // Uzyskaj warto�� krzywych B�ziera dla X i Z dla danego p�atu
                float curveValueX = bezierCurvesX[plat % bezierCurvesX.Length].Evaluate(Mathf.Repeat(x / waveLength, 1f));
                float curveValueZ = bezierCurvesZ[plat % bezierCurvesZ.Length].Evaluate(Mathf.Repeat(z / waveLength, 1f));

                // Zastosowanie fal na podstawie krzywych B�ziera dla danego p�atu
                vertex.y = Mathf.Sin(time + x * 0.5f + z * 0.3f) * curveValueX * curveValueZ * waveHeight;

                displacedVerts[i] = vertex;
            }

            // Zaktualizuj siatk� danego p�atu
            Mesh mesh = meshFiltersArray[plat].mesh;
            mesh.vertices = displacedVerts;
            mesh.RecalculateNormals();

            offset += Time.deltaTime * speed;
        }
    }

    public float GetWaveHeight(float x)
    {
        return amplitude * Mathf.Sin(x / length + offset);
    }

    public float GetWaveHeightAtPosition(Vector3 position)
    {
        float time = Time.time * waveSpeed;
        float x = position.x;
        float z = position.z;

        float totalHeight = 0f;

        for (int plat = 0; plat < meshFiltersArray.Length; plat++)
        {
            float curveValueX = bezierCurvesX[plat % bezierCurvesX.Length].Evaluate(Mathf.Repeat(x / waveLength, 1f));
            float curveValueZ = bezierCurvesZ[plat % bezierCurvesZ.Length].Evaluate(Mathf.Repeat(z / waveLength, 1f));

            totalHeight += Mathf.Sin(time + x * 0.5f + z * 0.3f) * curveValueX * curveValueZ * waveHeight;
        }

        return totalHeight / meshFiltersArray.Length;
    }

}