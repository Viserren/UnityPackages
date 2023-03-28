using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    //public RectTransform ClockFace;
    public TextMeshProUGUI Date, Time, Season, Week;

    //public Image weatherSprite;
    //public Sprite[] weatherSprites;

    private float sunStartingRotation;

    public Light sunLight;
    public float dayIntensity;
    public float nightIntensity;
    public AnimationCurve dayNightCurve;
    public AnimationCurve sunHeightCurve;

    private void Awake()
    {
        //startingRotation = ClockFace.eulerAngles.z;
        sunStartingRotation = sunLight.transform.eulerAngles.z;
    }

    public void UpdateDateTime(DateTime dateTime)
    {
        float t = (float)dateTime.Hours / 24f;
        float pos = (float)dateTime.CurrentWeek / 16;
        float newRotation = Mathf.Lerp(-180, 180, t);

        // Winter 20
        // Autumn 40
        // Spring 60
        // Summer 80

        Quaternion lowAngle = Quaternion.Euler(20, 0, 0) * Quaternion.Euler(0, newRotation + sunStartingRotation, 0);
        Quaternion highAngle = Quaternion.Euler(80, 0, 0) * Quaternion.Euler(0, newRotation + sunStartingRotation, 0);

        float sunPos = sunHeightCurve.Evaluate(pos);
        float sunIntensity = dayNightCurve.Evaluate(t);
        if (sunLight)
        {
            sunLight.transform.rotation = Quaternion.Lerp(lowAngle, highAngle, sunPos);
            sunLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, sunIntensity);
        }
    }

    public void UpdateUI(DateTime dateTime)
    {
        if (Date)
        {
            Date.text = dateTime.DateToString();
        }
        if (Time)
        {
            Time.text = dateTime.TimeAMPMToString();
        }
        if (Season)
        {
            Season.text = dateTime.Season.ToString();
        }
        if (Week)
        {
            Week.text = $"Week: {dateTime.CurrentWeek.ToString()}";
        }
    }
}
