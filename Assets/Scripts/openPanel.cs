using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openPanel : MonoBehaviour
{
    public GameObject panel;

    // �ndert die Sichtbarkeit des Panels
    public void TogglePanelVisibility()
    {
        panel.SetActive(!panel.activeSelf);
    }
}




