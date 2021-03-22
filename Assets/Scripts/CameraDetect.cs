using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetect : MonoBehaviour
{
    public static bool isCameraInWater = false;

    public Color waterColor;
    public float waterFogDensity;

    private Color originColor;
    private float originFogDensity;

    public void Start() {
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "MainCamera") {
            cameraInWater();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "MainCamera") {
            cameraOutWater();
        }
    }

    private void cameraInWater() {
        isCameraInWater = true;

        AudioManager.Instance.MusicUnderwater(); //Music has underwater effect

        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFogDensity;
        RenderSettings.fog = true;
    }

    protected void cameraOutWater() {
        isCameraInWater = false;

        AudioManager.Instance.MusicNotUnderwater();

        RenderSettings.fogColor = originColor;
        RenderSettings.fogDensity = originFogDensity;
        RenderSettings.fog = true;
    }
}
