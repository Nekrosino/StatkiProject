using UnityEngine;

public class ShipController : MonoBehaviour
{
    public BezierTest waterSurface; // Referencja do skryptu generuj¹cego wodê
    public float buoyancyFactor = 1.0f; // Wspó³czynnik wypornoœci

    void Update()
    {
        // Pobierz aktualn¹ pozycjê statku
        Vector3 position = transform.position;

        // Uzyskaj wysokoœæ fali w danym punkcie
        float waveHeight = waterSurface.GetWaveHeightAtPosition(position);

        // Dostosuj pozycjê statku na podstawie wysokoœci fali i wspó³czynnika wypornoœci
        position.y = waveHeight * buoyancyFactor;

        // Zaktualizuj pozycjê statku
        transform.position = position;
    }
}
