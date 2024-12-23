using System.Collections;
using UnityEngine;

public class ControllerRight : MonoBehaviour
{
    private Transform grabbedObject;
    private const float ScaleChangeRate = 0.003f; // Skalen Änderungsrate
    private const float MinScaleFactor = 0.1f;    // Minimaler Skalierungsfaktor
    private const float MaxScaleFactor = 1.5f;    // Maximaler Skalierungsfaktor
    private bool isFlagged = false;
    public GameObject gameObj;
    public bool isGrabbed;
    public MyLocation myLocation;

    private void Update()
    {
        if (isGrabbed)
        {
            HandleScaling();
        }
    }

    // Diese Funktion steuert die Skalierung des Objekts basierend auf den Eingaben der linken und rechten Index-Trigger am Controller. 
    private void HandleScaling()
    {
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && gameObj.transform.localScale.x <= MaxScaleFactor)
        {
            ModifyScale(ScaleChangeRate);
        }

        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && gameObj.transform.localScale.x >= MinScaleFactor)
        {
            ModifyScale(-ScaleChangeRate);
        }
    }

    // Die Skalierung des Objekts ändern
    private void ModifyScale(float scaleAmount)
    {
        gameObj.transform.localScale += new Vector3(scaleAmount, scaleAmount, scaleAmount);
    }

    public void SetIsGrabbed(bool isGrabbed)
    {
        this.isGrabbed = isGrabbed;
    }

    // Wird das Objekt erfasst, verändert es seine Größe
    public void SelectObject()
    {
        if (isGrabbed)
        {
            grabbedObject = gameObj.transform;

            if (grabbedObject != null && !isFlagged)
            {
                grabbedObject.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                isFlagged = true;
            }
        }
    }

    // Wenn das Objekt losgelassen wird, kehrt es in seine ursprüngliche Größe und Position zurück
    public void DeselectObject()
    {
        if (!isGrabbed)
        {
            isFlagged = false;
            StartCoroutine(ResetObjectAfterDelay());
        }
    }

    private IEnumerator ResetObjectAfterDelay()
    {
        yield return new WaitForSeconds(1); // Eine Sekunde warten

        if (gameObj.name == "Stegasaurus_20K")
        {
            myLocation.HandleButtonX(new Vector3(-5.0f, 0.0f, 5.0f)); // Als Eingabe werden die Koordinaten eines Punktes in jedem Raum gesendet
        }
        else if (gameObj.name == "Pachycephalasaurus_24K")
        {
            myLocation.HandleButtonX(new Vector3(5.0f, 0.0f, 5.0f));
        }
        else if(gameObj.name == "Rampaging T-Rex")
        {
            myLocation.HandleButtonX(new Vector3(5.0f, 0.0f, -5.0f));
        }
        else if(gameObj.name == "Raptor_Animated_FBX_5K")
        {
            myLocation.HandleButtonX(new Vector3(-5.0f, 0.0f, -5.0f));
        }
        else
        {
            Debug.LogWarning("Game Object is null");
        }
    }
}
