using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomInOut : MonoBehaviour
{
    private CinemachineFreeLook freeLook;
    private CinemachineFreeLook.Orbit[] originalOrbits;

    public float minZoom = 0.5f;
    public float maxZoom = 3.0f;

    private float zoomPercent;

    public void Awake() {
        freeLook = GetComponent<CinemachineFreeLook>();
        originalOrbits = new CinemachineFreeLook.Orbit[freeLook.m_Orbits.Length];
        zoomPercent = minZoom;

        for(int i = 0; i < freeLook.m_Orbits.Length; i++) {
            originalOrbits[i].m_Height = freeLook.m_Orbits[i].m_Height;
            originalOrbits[i].m_Radius = freeLook.m_Orbits[i].m_Radius;
        }
    }

    public void Update() {
        for(int i = 0; i < freeLook.m_Orbits.Length; i++) {
                freeLook.m_Orbits[i].m_Height = originalOrbits[i].m_Height * zoomPercent;
                freeLook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * zoomPercent;
        }
        zoomScrolling();
    }

    // mouse wheel to change zoom percentage
    private void zoomScrolling() {
        float scroll = Input.GetAxis("Mouse ScrollWheel") / 10;

        if(scroll > 0) {
            if (zoomPercent <= maxZoom) {
                zoomPercent += scroll;
            }
        }
        if(scroll < 0) {
            if(zoomPercent >= minZoom) {
                zoomPercent += scroll;
            }
        }
    }
}
