using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaneDetectionToggle : MonoBehaviour
{
    private ARPlaneManager planeManager;
    
    [SerializeField]
    private TextMeshProUGUI toggleButtonText;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        toggleButtonText.text = "Disable";
    }

    public void TogglePlaneDetetion()
    {
        planeManager.enabled = !planeManager.enabled;
        string toggleButtonMessage = "";

        if(planeManager.enabled)
        {
            toggleButtonMessage = "Disable";
            SetAllPlanesActive(true);
        }
        else
        {
            toggleButtonMessage = "Enable";
            SetAllPlanesActive(false);
        }

        toggleButtonText.text = toggleButtonMessage;
    }

    private void SetAllPlanesActive(bool val)
    {
        foreach(var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(val);
        }
    }

}
