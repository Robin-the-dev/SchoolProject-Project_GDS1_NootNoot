using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UIHats : MonoBehaviour
{
    public float time = 0.5f;

    public float delay = 0f;

    [SerializeField] private int hatNum;

    private bool collected;
    private Image image;
    private CanvasGroup canvasGroup;

    private int leanTweenID;

    [SerializeField] private GameObject text;

    // Start is called before the first frame update
    private void Awake()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        HatScripts.hatPickedUp += Activate;
    }

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            canvasGroup.alpha = 0;
            LeanTween.cancel(leanTweenID);
            leanTweenID = LeanTween.value(gameObject, UpdateBlur, 0, 1, time).setDelay(delay).setIgnoreTimeScale(true).id;
        }
    }

    private void UpdateBlur(float value)
    {
        canvasGroup.alpha = value;
    }

    private void Activate(int num)
    {
        if (num ==hatNum)
        {
            image.color = Color.white;
            text.SetActive(true);
            Debug.Log(hatNum);
        }
    }
}
