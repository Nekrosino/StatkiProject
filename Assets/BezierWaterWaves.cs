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
