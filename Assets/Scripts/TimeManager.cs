using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class TimeManager : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments caVol;

    private float duration = 7.5f;

    private Color MORNING_COLOR = new Color(200 / 255f, 200 / 255f, 255 / 255f);
    private Color NOON_COLOR = new Color(255 / 255f, 255 / 255f, 255 / 255f);
    private Color EVENING_COLOR = new Color(255 / 255f, 150 / 255f, 255 / 255f);
    private Color NIGHT_COLOR = new Color(150 / 255f, 150 / 255f, 255 / 255f);

    private float maxLightVol = 1.0f;
    private float minLightVol = 0.2f;
    public Light2D globalLight;
    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet(out caVol);
        caVol.colorFilter.value = MORNING_COLOR;
        Debug.Log(caVol.colorFilter.value);
        globalLight.intensity = (maxLightVol + minLightVol) / 2f;
        ToNOON();
    }

    void ToNOON()
    {
        DOTween.To(() => globalLight.intensity, (x) => globalLight.intensity = x, maxLightVol, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => caVol.colorFilter.value, (x) => caVol.colorFilter.value = x, NOON_COLOR, duration).SetEase(Ease.InOutSine).OnComplete(ToNOONCallback);
    }

    void ToEVENING()
    {
        DOTween.To(() => globalLight.intensity, (x) => globalLight.intensity = x, minLightVol, duration*2).SetEase(Ease.InOutSine);
        DOTween.To(() => caVol.colorFilter.value, (x) => caVol.colorFilter.value = x, EVENING_COLOR, duration).SetEase(Ease.InOutSine).OnComplete(ToEVENINGCallback);
    }

    void ToNIGHT()
    {
        DOTween.To(() => caVol.colorFilter.value, (x) => caVol.colorFilter.value = x, NIGHT_COLOR, duration).SetEase(Ease.InOutSine).OnComplete(ToNIGHTCallback);
    }

    void ToMORNING()
    {
        DOTween.To(() => globalLight.intensity, (x) => globalLight.intensity = x, (maxLightVol + minLightVol) / 2f, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => caVol.colorFilter.value, (x) => caVol.colorFilter.value = x, MORNING_COLOR, duration).SetEase(Ease.InOutSine).OnComplete(ToMORNINGCallback);
    }

    void ToNOONCallback()
    {
        ToEVENING();
    }

    void ToEVENINGCallback()
    {
        ToNIGHT();
    }

    void ToNIGHTCallback()
    {
        ToMORNING();
    }

    void ToMORNINGCallback()
    {
        ToNOON();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
