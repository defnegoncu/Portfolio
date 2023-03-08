using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour{

    [Header("Time")]
    [Tooltip("Day Length in Minutes")]

    //length of day in minutes
    [SerializeField] private float _targetDayLength = 0.5f; 
    //geter for dayLength
    public float targetDayLength{
        get{
            return _targetDayLength;
        }
    }

    [SerializeField] private float elapsedTime;

    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay;
    public float timeOfDay{
        get{
            return _timeOfDay;
        }
    }

    private float _timeScale = 100f;

    public bool pause = false; //pause the cycle without pausing the game

    [SerializeField] private AnimationCurve timeCurve;
    private float timeCurveNormalization;

    [Header("Sun Light")]
    [SerializeField] private Transform dailyRotation;
    [SerializeField] private Light sun;
    [SerializeField] private Light moon;
    [SerializeField] private float sunBaseIntensity = 1f;
    [SerializeField] private float sunVariation = 1.5f;
    [SerializeField] private Gradient sunColor;
    private bool moonEnableAgain;
    private bool moonDisable;
    private bool sunDisable;
    private float intensity;
    private bool makeDayShorter;

    private void Update(){

        if (!pause){
            UpdateTimeScale();
            UpdateTime();
        }

        AdjustSunRotation();
        SunIntensity();
        AdjustSunColor();
    }

    private void Start(){
        NormalizedTimeCurve();
    }

    private void UpdateTimeScale(){

        _timeScale = 24 / (_targetDayLength / 60);
        _timeScale *= timeCurve.Evaluate(elapsedTime / (targetDayLength * 60)); //changes timescale based on time curve
        _timeScale /= timeCurveNormalization; //keeps day length at target value
    }

    private void NormalizedTimeCurve(){

        float stepSize = 0.01f;
        int numberSteps = Mathf.FloorToInt(1.0f / stepSize);
        float curveTotal = 0.0f;

        for (int i = 0; i < numberSteps; i++){
            curveTotal += timeCurve.Evaluate(i * stepSize);
        }

        timeCurveNormalization = curveTotal / numberSteps; //keeps day length at target value
    }

    private void UpdateTime(){
        
        _timeOfDay += Time.deltaTime * _timeScale / 86400.0f; // seconds in a day
        elapsedTime += Time.deltaTime;
    }

    //rotates the sun daily
    private void AdjustSunRotation() {

        float sunAngle = (timeOfDay * 360.0f) - 24.0f;

        if (sunAngle > 20.0f){
            sunAngle += 125.0f;
        }

        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(sunAngle, 280.0f, 0.0f));

        if (moonDisable && !moonEnableAgain && sunAngle > 180){
            moon.enabled = !moon.enabled;
            moonEnableAgain = true;
        }

        if (!moonDisable && sunAngle > 5.0f){
            moon.enabled = !moon.enabled;
            moonDisable = true;
            
        }

        if (!sunDisable && sunAngle > 205){
            sun.enabled = !sun.enabled;
            sunDisable = true;
            return;
        }
    }

    private void SunIntensity(){
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down) + 0.05f;
        intensity = Mathf.Clamp01(intensity);

        sun.intensity = intensity * sunVariation + sunBaseIntensity;
    }

    private void AdjustSunColor()
    {
        sun.color = sunColor.Evaluate(intensity);
    }
}