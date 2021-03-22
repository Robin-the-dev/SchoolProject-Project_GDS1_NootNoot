using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[AddComponentMenu("UI/Blur Panel")]
public class BlurPanel : Image
{
    public bool animate;

    public float time = 0.5f;

    public float delay = 0f;

    private CanvasGroup canvasGroup;
    private static readonly int Size = Shader.PropertyToID("_Size");

    private void Reset()
    {
        color = Color.black * 0.1f;
    }

    protected override void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        PauseManager.gamePaused += Activate;
    }

    public void Activate()
    {
        if (Application.isPlaying)
        {
            material.SetFloat(Size, 0);
            canvasGroup.alpha = 0;
            LeanTween.value(gameObject, UpdateBlur, 0, 1, time).setDelay(delay).setIgnoreTimeScale(true);
        }
    }

    private void UpdateBlur(float value)
    {
        material.SetFloat(Size, value);
        canvasGroup.alpha = value;
    }
}
