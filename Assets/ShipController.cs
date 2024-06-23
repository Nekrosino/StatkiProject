using UnityEngine;

public class ShipController : MonoBehaviour
{
    public BezierTest waterSurface; // Referencja do skryptu generuj�cego wod�
    public float buoyancyFactor = 1.0f; // Wsp�czynnik wyporno�ci

    void Update()
    {
        // Pobierz aktualn� pozycj� statku
        Vector3 position = transform.position;

        // Uzyskaj wysoko�� fali w danym punkcie
        float waveHeight = waterSurface.GetWaveHeightAtPosition(position);

        // Dostosuj pozycj� statku na podstawie wysoko�ci fali i wsp�czynnika wyporno�ci
        position.y = waveHeight * buoyancyFactor;

        // Zaktualizuj pozycj� statku
        transform.position = position;
    }
}
