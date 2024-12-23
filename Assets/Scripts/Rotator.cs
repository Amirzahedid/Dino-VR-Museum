using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    public GameObject objectToRotate;
    public Slider speedSlider;       

    private float rotationSpeed = 10f; 
    public bool isRotating = false;   

    void Update()
    {
        // Prüft, ob Rotation aktiviert ist
        if (isRotating && objectToRotate != null)
        {
            Debug.Log(objectToRotate.tag);
            objectToRotate.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    // Passt die Geschwindigkeit basierend auf dem Wert des Schiebereglers an
    public void AdjustRotationSpeed()
    {
        rotationSpeed = speedSlider.value; 
    }

    // Funktion zum Umschalten der Rotation
    public void ToggleRotation()
    {
        isRotating = !isRotating; 
    }
}